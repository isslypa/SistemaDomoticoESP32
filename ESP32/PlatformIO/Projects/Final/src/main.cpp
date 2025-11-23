#include <Arduino.h>
#include <Wire.h>

// =========================
// Configuración de pines para ESP32 DevKit V1 (38 pines)
// =========================

// LED de estado / lámpara 1
const int LED1_PIN = 2; // GPIO2 - LED interno en ESP32 DevKit V1

// Motor DC con L298N (canal A)
const int M1_EN_PIN = 23;  // ENA - PWM para velocidad
const int M1_IN1_PIN = 19; // IN1 - dirección
const int M1_IN2_PIN = 18; // IN2 - dirección

// LM75 (I2C) - Usando pines I2C por defecto del DevKit V1
// SDA = GPIO 21
// SCL = GPIO 22
const uint8_t LM75_ADDR = 0x48; // Dirección típica del LM75
const int SDA_PIN = 21; // SDA por defecto
const int SCL_PIN = 22; // SCL por defecto

// =========================
// Variables de estado
// =========================

bool led1On = false;
int m1Speed = 0; // -255..255
String lineBuffer;

// =========================
// Funciones auxiliares
// =========================

void setLed1(bool on) {
  led1On = on;
  digitalWrite(LED1_PIN, on ? HIGH : LOW);
}

void stopMotor() {
  m1Speed = 0;
  // PWM en 0
  ledcWrite(0, 0); // Canal 0
  // Frenado/coast: ambas líneas en LOW
  digitalWrite(M1_IN1_PIN, LOW);
  digitalWrite(M1_IN2_PIN, LOW);
}

void setMotorSpeed(int value) {
  // value en el rango -255..255
  m1Speed = value;
  
  if (value == 0) {
    stopMotor();
    return;
  }
  
  bool forward = (value > 0);
  int pwm = abs(value);
  if (pwm > 255) pwm = 255;
  
  if (forward) {
    digitalWrite(M1_IN1_PIN, HIGH);
    digitalWrite(M1_IN2_PIN, LOW);
  } else {
    digitalWrite(M1_IN1_PIN, LOW);
    digitalWrite(M1_IN2_PIN, HIGH);
  }
  
  // PWM en canal 0
  ledcWrite(0, pwm);
}

float readLM75Temperature() {
  // Lectura básica del registro de temperatura del LM75
  // Formato típico: 9 a 11 bits, resolución 0.5 °C
  Wire.beginTransmission(LM75_ADDR);
  Wire.write(0x00); // Registro de temperatura
  if (Wire.endTransmission(false) != 0) {
    return NAN; // Error en la transmisión
  }
  
  int bytesReceived = Wire.requestFrom((int)LM75_ADDR, 2);
  
  if (bytesReceived < 2) {
    return NAN;
  }
  
  uint8_t msb = Wire.read();
  uint8_t lsb = Wire.read();
  
  // Conversión sencilla según formato LM75 (0.5°C por bit, 9 bits)
  int16_t raw = ((int16_t)msb << 8) | lsb;
  // Desplazar para obtener 9 bits significativos
  raw >>= 7; // Cada unidad = 0.5 °C
  float tempC = raw * 0.5f;
  
  return tempC;
}

void sendState() {
  Serial.print("STATE:LED1=");
  Serial.print(led1On ? 1 : 0);
  Serial.print(",M1=");
  Serial.println(m1Speed);
}

// =========================
// Procesamiento de comandos
// =========================

void processCommand(const String& cmd) {
  String c = cmd;
  c.trim();
  
  if (c.length() == 0) return;
  
  if (c == "PING") {
    Serial.println("PONG");
    return;
  }
  
  if (c == "LED1:ON") {
    setLed1(true);
    Serial.println("OK");
    return;
  }
  
  if (c == "LED1:OFF") {
    setLed1(false);
    Serial.println("OK");
    return;
  }
  
  if (c == "M1:STOP") {
    stopMotor();
    Serial.println("OK");
    return;
  }
  
  if (c == "STATE?") {
    sendState();
    return;
  }
  
  if (c == "TEMP?") {
    float t = readLM75Temperature();
    if (isnan(t)) {
      Serial.println("ERR:TEMP");
    } else {
      Serial.print("TEMP:");
      Serial.println(t, 2); // 2 decimales
    }
    return;
  }
  
  // Comando de velocidad: M1:SET:<valor>
  if (c.startsWith("M1:SET:")) {
    String valStr = c.substring(7); // strlen("M1:SET:") = 7
    valStr.trim();
    int value = valStr.toInt(); // toInt maneja signos
    
    if (value < -255 || value > 255) {
      Serial.println("ERR:VAL");
      return;
    }
    
    setMotorSpeed(value);
    Serial.println("OK");
    return;
  }
  
  // Si el comando no coincide con ninguno
  Serial.println("ERR");
}

// =========================
// setup y loop
// =========================

void setup() {
  Serial.begin(115200);
  delay(1000); // pequeña espera para estabilidad
  
  // Configurar LED
  pinMode(LED1_PIN, OUTPUT);
  setLed1(false);
  
  // Configurar pines del motor
  pinMode(M1_IN1_PIN, OUTPUT);
  pinMode(M1_IN2_PIN, OUTPUT);
  
  // Configurar PWM en canal 0, frecuencia 10 kHz, resolución 8 bits
  ledcSetup(0, 10000, 8);
  ledcAttachPin(M1_EN_PIN, 0);
  
  stopMotor();
  
  // Inicializar I2C con pines por defecto
  Wire.begin(SDA_PIN, SCL_PIN);
  Wire.setClock(100000); // 100 kHz para I2C estándar
  
  Serial.println("READY");
  Serial.println("ESP32 DevKit V1 - Sistema Domotico");
  Serial.print("LED: GPIO");
  Serial.println(LED1_PIN);
  Serial.print("Motor: ENA=GPIO");
  Serial.print(M1_EN_PIN);
  Serial.print(", IN1=GPIO");
  Serial.print(M1_IN1_PIN);
  Serial.print(", IN2=GPIO");
  Serial.println(M1_IN2_PIN);
  Serial.print("I2C: SDA=GPIO");
  Serial.print(SDA_PIN);
  Serial.print(", SCL=GPIO");
  Serial.println(SCL_PIN);
}

void loop() {
  // Lectura de líneas desde el puerto serial
  while (Serial.available()) {
    char ch = (char)Serial.read();
    
    if (ch == '\n' || ch == '\r') {
      if (lineBuffer.length() > 0) {
        processCommand(lineBuffer);
        lineBuffer = "";
      }
    } else {
      lineBuffer += ch;
      // Protección simple ante desbordes
      if (lineBuffer.length() > 80) {
        lineBuffer = "";
        Serial.println("ERR:OVERFLOW");
      }
    }
  }
}