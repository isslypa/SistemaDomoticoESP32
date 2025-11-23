# Sistema de Control Domótico ESP32 + C#

**Documentación Completa**

---

## 📋 Índice

1. [Introducción](#1-introducción)
2. [Descripción del Sistema](#2-descripción-del-sistema)
3. [Arquitectura del Sistema](#3-arquitectura-del-sistema)
4. [Componentes de Software](#4-componentes-de-software)
5. [Componentes de Hardware](#5-componentes-de-hardware)
6. [Protocolo de Comunicación](#6-protocolo-de-comunicación)
7. [Implementación](#7-implementación)
8. [Pruebas Realizadas](#8-pruebas-realizadas)
9. [Resultados](#9-resultados)
10. [Conclusiones](#10-conclusiones)
11. [Referencias](#11-referencias)

---

## 1. Introducción

### 1.1 Contexto

El presente proyecto implementa un **sistema de control domótico** que permite la gestión remota de dispositivos electrónicos mediante una interfaz gráfica desarrollada en **C# con Windows Forms** y un microcontrolador **ESP32**. El sistema establece comunicación serial bidireccional para controlar actuadores (LEDs, motores DC) y leer sensores (temperatura).

### 1.2 Objetivos

- **Objetivo General**: Desarrollar un sistema de control domótico funcional con interfaz gráfica y hardware embebido.

- **Objetivos Específicos**:
  - Implementar comunicación serial entre PC y ESP32 a 115200 bps
  - Crear una interfaz gráfica intuitiva para gestión de dispositivos
  - Controlar actuadores digitales (LEDs) y PWM (motor DC)
  - Integrar sensor de temperatura mediante protocolo I2C
  - Implementar registro de eventos en tiempo real (log)
  - Documentar protocolo de comunicación

### 1.3 Alcance

El sistema permite:
- ✅ Crear y administrar múltiples dispositivos virtuales (lámparas)
- ✅ Control de LED mediante GPIO del ESP32
- ✅ Control bidireccional de motor DC (adelante/reversa/stop)
- ✅ Lectura de temperatura del sensor LM75 por I2C
- ✅ Registro completo de eventos con timestamps
- ✅ Comunicación serial robusta con manejo de errores

---

## 2. Descripción del Sistema

### 2.1 Características Principales

#### Interfaz Gráfica (Windows Forms)
- **Gestión de dispositivos**: Agregar, listar, encender/apagar dispositivos
- **Control de motor DC**: Ajuste de velocidad (-255 a 255) y dirección
- **Monitoreo de temperatura**: Solicitud y visualización de temperatura en °C
- **Log de eventos**: Registro con timestamp de todas las interacciones
- **Conexión serial**: Detección automática de puertos COM, conexión/desconexión

#### Firmware ESP32 (C++ / Arduino)
- **Gestión de comandos**: Parser de comandos de texto
- **Control de actuadores**: GPIO para LED, PWM para motor con L298N
- **Lectura de sensores**: Comunicación I2C con sensor LM75
- **Respuestas estructuradas**: Confirmaciones OK/ERR, valores de temperatura

### 2.2 Tecnologías Utilizadas

| Componente | Tecnología | Versión |
|------------|-----------|---------|
| Aplicación PC | C# .NET | 8.0 |
| Framework GUI | Windows Forms | .NET 8.0 |
| IDE PC | Visual Studio | 2022 |
| Microcontrolador | ESP32 DevKit V1 | - |
| IDE Embebido | PlatformIO | VSCode |
| Framework ESP32 | Arduino Framework | - |
| Comunicación | UART Serial | 115200 bps |
| Protocolo I2C | Wire Library | 100 kHz |

---

## 3. Arquitectura del Sistema

### 3.1 Arquitectura General

```
┌─────────────────────────────────────────────────────────────┐
│                     CAPA DE PRESENTACIÓN                    │
│  ┌──────────────────────────────────────────────────────┐  │
│  │              Windows Forms (UI)                       │  │
│  │  - Form1.cs (Interfaz gráfica)                       │  │
│  │  - Controles: TextBox, ListBox, Buttons, Labels      │  │
│  │  - ErrorProvider, TrackBar                           │  │
│  └──────────────────────────────────────────────────────┘  │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        ↓
┌─────────────────────────────────────────────────────────────┐
│                     CAPA DE LÓGICA                          │
│  ┌──────────────────────────────────────────────────────┐  │
│  │              Domain (Modelo de Negocio)               │  │
│  │  - IActuable (Interfaz)                              │  │
│  │  - DispositivoBase (Clase abstracta)                 │  │
│  │  - Lampara (Implementación concreta)                 │  │
│  │  - ControladorDomotico (Gestor de dispositivos)      │  │
│  └──────────────────────────────────────────────────────┘  │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        ↓
┌─────────────────────────────────────────────────────────────┐
│                  CAPA DE COMUNICACIÓN                       │
│  ┌──────────────────────────────────────────────────────┐  │
│  │         SerialPort (System.IO.Ports)                  │  │
│  │  - Configuración: 115200 bps, N, 8, 1               │  │
│  │  - DataReceived event handler                        │  │
│  │  - WriteLine() / ReadLine()                          │  │
│  └──────────────────────────────────────────────────────┘  │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        │ USB-Serial
                        │ (UART)
                        ↓
┌─────────────────────────────────────────────────────────────┐
│                  CAPA DE HARDWARE                           │
│  ┌──────────────────────────────────────────────────────┐  │
│  │              ESP32 DevKit V1                          │  │
│  │  - main.cpp (Loop principal)                         │  │
│  │  - processCommand() (Parser)                         │  │
│  │  - Control GPIO (LED)                                │  │
│  │  - Control PWM (Motor)                               │  │
│  │  - Comunicación I2C (Sensor)                         │  │
│  └──────────────────────────────────────────────────────┘  │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        ↓
┌─────────────────────────────────────────────────────────────┐
│                  DISPOSITIVOS FÍSICOS                       │
│  - LED (GPIO2 - Integrado)                                 │
│  - Motor DC + Driver L298N (GPIO23, 19, 18)               │
│  - Sensor LM75 (I2C - GPIO21 SDA, GPIO22 SCL)             │
└─────────────────────────────────────────────────────────────┘
```

### 3.2 Patrón de Diseño Aplicado

#### Patrón Orientado a Objetos (OOP)

**Interfaz `IActuable`**:
- Define contrato para dispositivos actuables
- Métodos: `Encender()`, `Apagar()`
- Propiedad: `EstaEncendido`

**Clase Abstracta `DispositivoBase`**:
- Implementa `IActuable`
- Proporciona funcionalidad común
- Gestiona estado encendido/apagado
- Proporciona nombre del dispositivo

**Clase Concreta `Lampara`**:
- Hereda de `DispositivoBase`
- Puede extenderse con lógica específica

**Ventajas**:
- ✅ Extensibilidad: Fácil agregar nuevos tipos de dispositivos
- ✅ Polimorfismo: Tratamiento uniforme de dispositivos
- ✅ Encapsulamiento: Estado interno protegido

### 3.3 Flujo de Datos

```
Usuario → GUI → ControladorDomotico → SerialPort → ESP32 → Actuador/Sensor
                                                      ↓
Usuario ← GUI ← Log ← DataReceived ← SerialPort ← ESP32 ← Actuador/Sensor
```

---

## 4. Componentes de Software

### 4.1 Estructura del Proyecto C#

```
ControlDomotico/
├── Program.cs              # Punto de entrada
├── Domain/                 # Lógica de negocio
│   ├── IActuable.cs       # Interfaz de dispositivos
│   ├── DispositivoBase.cs # Clase base abstracta
│   ├── Lampara.cs         # Implementación de lámpara
│   └── ControladorDomotico.cs # Gestor de dispositivos
├── UI/                     # Interfaz de usuario
│   ├── Form1.cs           # Lógica de la interfaz
│   ├── Form1.Designer.cs  # Diseño visual
│   └── Form1.resx         # Recursos
└── Properties/             # Configuración del proyecto
```

### 4.2 Clases Principales

#### 4.2.1 IActuable (Interfaz)

```csharp
public interface IActuable
{
    void Encender();
    void Apagar();
    bool EstaEncendido { get; }
}
```

**Propósito**: Define el contrato para todos los dispositivos actuables.

#### 4.2.2 DispositivoBase (Clase Abstracta)

```csharp
public abstract class DispositivoBase : IActuable
{
    public string Nombre { get; }
    protected bool encendido;
    
    protected DispositivoBase(string nombre)
    {
        Nombre = string.IsNullOrWhiteSpace(nombre) ? "Dispositivo" : nombre.Trim();
        encendido = false;
    }
    
    public virtual void Encender() => encendido = true;
    public virtual void Apagar() => encendido = false;
    public bool EstaEncendido => encendido;
    public override string ToString() => $"{Nombre} (On: {encendido})";
}
```

**Características**:
- Proporciona implementación base para dispositivos
- Gestiona nombre y estado
- Métodos virtuales para permitir override

#### 4.2.3 Lampara (Clase Concreta)

```csharp
public sealed class Lampara : DispositivoBase
{
    public Lampara(string nombre) : base(nombre) { }
    
    public override void Encender()
    {
        base.Encender();
        // Lógica adicional si es necesaria
    }
    
    public override void Apagar()
    {
        base.Apagar();
        // Lógica adicional si es necesaria
    }
}
```

**Características**:
- Implementación específica de lámpara
- Sealed para evitar herencia adicional
- Puede extenderse con lógica específica

#### 4.2.4 ControladorDomotico (Gestor)

```csharp
public class ControladorDomotico
{
    private readonly List<IActuable> _dispositivos = new();
    
    public bool Agregar(IActuable d)
    {
        if (d is null) return false;
        
        bool yaExiste = _dispositivos.Any(x =>
            x is DispositivoBase b &&
            d is DispositivoBase nb &&
            string.Equals(b.Nombre, nb.Nombre, StringComparison.OrdinalIgnoreCase));
        
        if (yaExiste) return false;
        
        _dispositivos.Add(d);
        return true;
    }
    
    public IReadOnlyList<IActuable> Dispositivos => _dispositivos.AsReadOnly();
    
    public void EncenderTodos()
    {
        foreach (var x in _dispositivos) x.Encender();
    }
    
    public void ApagarTodos()
    {
        foreach (var x in _dispositivos) x.Apagar();
    }
}
```

**Características**:
- Gestiona colección de dispositivos
- Evita duplicados por nombre
- Operaciones masivas (encender/apagar todos)

### 4.3 Estructura del Firmware ESP32

```
ESP32/PlatformIO/Projects/Final/
├── platformio.ini    # Configuración del proyecto
├── src/
│   └── main.cpp     # Código principal
├── include/         # Headers (vacío)
├── lib/             # Librerías personalizadas (vacío)
└── test/            # Pruebas unitarias
```

### 4.4 Funciones Principales del Firmware

#### 4.4.1 setup()

```cpp
void setup() {
    Serial.begin(115200);
    
    // Configurar LED
    pinMode(LED1_PIN, OUTPUT);
    setLed1(false);
    
    // Configurar Motor
    pinMode(M1_IN1_PIN, OUTPUT);
    pinMode(M1_IN2_PIN, OUTPUT);
    ledcSetup(0, 10000, 8);      // Canal 0, 10kHz, 8 bits
    ledcAttachPin(M1_EN_PIN, 0);
    
    // Configurar I2C
    Wire.begin(SDA_PIN, SCL_PIN);
    Wire.setClock(100000);
    
    Serial.println("READY");
    // Información de configuración...
}
```

#### 4.4.2 loop()

```cpp
void loop() {
    while (Serial.available()) {
        char ch = (char)Serial.read();
        
        if (ch == '\n' || ch == '\r') {
            if (lineBuffer.length() > 0) {
                processCommand(lineBuffer);
                lineBuffer = "";
            }
        } else {
            lineBuffer += ch;
            if (lineBuffer.length() > 80) {
                lineBuffer = "";
                Serial.println("ERR:OVERFLOW");
            }
        }
    }
}
```

#### 4.4.3 processCommand()

```cpp
void processCommand(const String& cmd) {
    String c = cmd;
    c.trim();
    
    if (c == "LED1:ON") {
        setLed1(true);
        Serial.println("OK");
    }
    else if (c == "LED1:OFF") {
        setLed1(false);
        Serial.println("OK");
    }
    else if (c.startsWith("M1:SET:")) {
        String valStr = c.substring(7);
        int value = valStr.toInt();
        if (value < -255 || value > 255) {
            Serial.println("ERR:VAL");
            return;
        }
        setMotorSpeed(value);
        Serial.println("OK");
    }
    else if (c == "TEMP?") {
        float t = readLM75Temperature();
        if (isnan(t)) {
            Serial.println("ERR:TEMP");
        } else {
            Serial.print("TEMP:");
            Serial.println(t, 2);
        }
    }
    else {
        Serial.println("ERR");
    }
}
```

---

## 5. Componentes de Hardware

### 5.1 Lista de Materiales

| Componente | Descripción | Cantidad |
|------------|-------------|----------|
| ESP32 DevKit V1 | Microcontrolador con 38 pines | 1 |
| Módulo L298N | Driver de motor DC dual | 1 |
| Motor DC | 6V-12V con reductora | 1 |
| Sensor LM75 | Sensor de temperatura I2C | 1 |
| Cables Jumper | Macho-Macho | 15 |
| Cable USB | Para programación y alimentación | 1 |
| Fuente Externa | 9V-12V para motor (opcional) | 1 |

### 5.2 Tabla Completa de Conexiones del Hardware

#### Conexiones del LED

| Componente Origen | Pin Origen | Cable | Componente Destino | Pin Destino | Función |
|-------------------|------------|-------|-------------------|-------------|---------|
| ESP32 DevKit V1 | GPIO2 | - | LED Integrado | Ánodo | Control ON/OFF |
| ESP32 DevKit V1 | GND | Negro | LED Integrado | Cátodo | Tierra |

#### Conexiones del Motor DC (L298N)

| Componente Origen | Pin Origen | Cable | Componente Destino | Pin Destino | Función |
|-------------------|------------|-------|-------------------|-------------|---------|
| ESP32 DevKit V1 | GPIO23 | Naranja | L298N | ENA | PWM - Control de velocidad |
| ESP32 DevKit V1 | GPIO19 | Amarillo | L298N | IN1 | Control dirección 1 |
| ESP32 DevKit V1 | GPIO18 | Verde | L298N | IN2 | Control dirección 2 |
| ESP32 DevKit V1 | GND | Negro | L298N | GND | Tierra común |
| L298N | OUT1 | Rojo | Motor DC | Terminal + | Alimentación motor |
| L298N | OUT2 | Negro | Motor DC | Terminal - | Tierra motor |
| Fuente Externa (9-12V) | + | Rojo | L298N | +12V | Alimentación motor |
| Fuente Externa (9-12V) | - | Negro | L298N | GND | Tierra |

#### Conexiones del Sensor de Temperatura (LM75)

| Componente Origen | Pin Origen | Cable | Componente Destino | Pin Destino | Función |
|-------------------|------------|-------|-------------------|-------------|---------|
| ESP32 DevKit V1 | GPIO21 | Azul | LM75 | SDA | Datos I2C |
| ESP32 DevKit V1 | GPIO22 | Blanco | LM75 | SCL | Clock I2C |
| ESP32 DevKit V1 | 5V | Rojo | LM75 | VCC | Alimentación 5V |
| ESP32 DevKit V1 | GND | Negro | LM75 | GND | Tierra |

#### Conexiones de Alimentación

| Componente | Pin | Fuente | Voltaje | Notas |
|------------|-----|--------|---------|-------|
| ESP32 DevKit V1 | USB / VIN | PC / Adaptador | 5V | Alimentación principal |
| LM75 | VCC | ESP32 (5V) | 5V | Puede usar 3.3V también |
| L298N (Lógica) | 5V | ESP32 (5V) | 5V | Opcional si usa fuente externa |
| L298N (Motor) | +12V | Fuente externa | 9-12V | Para el motor DC |
| Motor DC | - | L298N OUT1/OUT2 | 6-12V | Según especificación |

### 5.3 Resumen de Pines Utilizados del ESP32

| Pin GPIO | Función | Conectado a | Tipo de señal |
|----------|---------|-------------|---------------|
| GPIO2 | LED Control | LED Integrado | Digital Output |
| GPIO18 | Motor IN2 | L298N IN2 | Digital Output |
| GPIO19 | Motor IN1 | L298N IN1 | Digital Output |
| GPIO21 | I2C SDA | LM75 SDA | I2C Data |
| GPIO22 | I2C SCL | LM75 SCL | I2C Clock |
| GPIO23 | Motor PWM | L298N ENA | PWM Output |
| GND | Tierra | Común a todos | Ground |
| 5V | Alimentación | LM75 VCC | Power |

### 5.4 Notas Importantes de Conexión

⚠️ **Advertencias**:
- **NO** conectar el motor directamente al ESP32, siempre usar el driver L298N
- **Verificar** polaridad de alimentación antes de conectar
- **Tierra común**: Todos los GND deben estar conectados
- **Voltaje del motor**: Verificar que sea compatible con tu motor DC (6-12V típico)
- **Corriente**: El L298N soporta hasta 2A por canal

✅ **Recomendaciones**:
- Usar cables de colores diferentes para cada función
- Etiquetar las conexiones
- Verificar continuidad con multímetro
- Probar cada componente individualmente antes de conectar todo
- Usar fuente externa para el motor (no la USB del ESP32)

### 5.5 Especificaciones Técnicas

#### ESP32 DevKit V1
- **Microcontrolador**: Espressif ESP32
- **CPU**: Dual-core Tensilica LX6 @ 240 MHz
- **RAM**: 520 KB SRAM
- **Flash**: 4 MB
- **GPIO**: 30 pines disponibles
- **Comunicación**: UART, I2C, SPI, I2S
- **PWM**: 16 canales
- **Alimentación**: 5V vía USB

#### Módulo L298N
- **Voltaje lógico**: 5V
- **Voltaje motor**: 5V - 35V
- **Corriente máxima**: 2A por canal
- **Canales**: 2 (dual motor)
- **Protección**: Diodos de flyback integrados

#### Sensor LM75
- **Protocolo**: I2C
- **Dirección**: 0x48 (configurable)
- **Rango**: -55°C a +125°C
- **Resolución**: 0.5°C (9 bits)
- **Precisión**: ±2°C (típico)
- **Voltaje**: 3.3V - 5V

---

## 6. Protocolo de Comunicación

### 6.1 Especificaciones

- **Velocidad**: 115200 bps
- **Bits de datos**: 8
- **Paridad**: Ninguna
- **Bits de parada**: 1
- **Control de flujo**: Ninguno
- **Terminador**: `\n` (Line Feed)

### 6.2 Formato de Comandos

Todos los comandos siguen el formato:
```
DISPOSITIVO:ACCION[:PARAMETRO]
```

### 6.3 Tabla de Comandos Completa

| Comando | Parámetros | Respuesta | Descripción |
|---------|-----------|-----------|-------------|
| `LED1:ON` | - | `OK` | Enciende LED |
| `LED1:OFF` | - | `OK` | Apaga LED |
| `M1:SET:<val>` | -255 a 255 | `OK` / `ERR:VAL` | Velocidad motor |
| `M1:STOP` | - | `OK` | Detiene motor |
| `TEMP?` | - | `TEMP:<val>` / `ERR:TEMP` | Lee temperatura |
| `PING` | - | `PONG` | Test conectividad |
| `STATE?` | - | `STATE:...` | Estado completo |

### 6.4 Códigos de Error

| Código | Descripción |
|--------|-------------|
| `ERR` | Comando no reconocido |
| `ERR:VAL` | Valor fuera de rango |
| `ERR:TEMP` | Error en sensor |
| `ERR:OVERFLOW` | Buffer lleno |

---

## 7. Implementación

### 7.1 Gestión de Comunicación Serial (C#)

#### Inicialización

```csharp
private void Conectar()
{
    _sp.PortName = cmbPuertos.SelectedItem.ToString();
    _sp.BaudRate = 115200;
    _sp.NewLine = "\n";
    _sp.Open();
    
    lblEstadoConexion.Text = $"Conectado a {_sp.PortName}";
    Log($"Conexión establecida en {_sp.PortName}");
}
```

#### Envío de Comandos

```csharp
private void EnviarComando(string comando)
{
    if (!_sp.IsOpen) return;
    
    _sp.WriteLine(comando);
    Log($">> {comando}");
}
```

#### Recepción de Datos

```csharp
private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
{
    string linea = _sp.ReadLine().Trim();
    
    this.BeginInvoke(new Action(() =>
    {
        Log($"<< {linea}");
        
        if (linea.StartsWith("TEMP:"))
        {
            string tempStr = linea.Substring(5).Trim();
            if (float.TryParse(tempStr, out float temp))
            {
                lblTemperaturaValor.Text = $"{temp:F2} °C";
            }
        }
    }));
}
```

### 7.2 Control de Motor (Firmware)

```cpp
void setMotorSpeed(int value) {
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
    
    ledcWrite(0, pwm);
}
```

### 7.3 Lectura de Temperatura (Firmware)

```cpp
float readLM75Temperature() {
    Wire.beginTransmission(LM75_ADDR);
    Wire.write(0x00);
    if (Wire.endTransmission(false) != 0) {
        return NAN;
    }
    
    Wire.requestFrom((int)LM75_ADDR, 2);
    if (Wire.available() < 2) {
        return NAN;
    }
    
    uint8_t msb = Wire.read();
    uint8_t lsb = Wire.read();
    
    int16_t raw = ((int16_t)msb << 8) | lsb;
    raw >>= 7;
    float tempC = raw * 0.5f;
    
    return tempC;
}
```

---

## 8. Pruebas Realizadas

### 8.1 Plan de Pruebas

| ID | Tipo | Descripción | Criterio de Éxito |
|----|------|-------------|-------------------|
| T01 | Unitaria | Agregar dispositivo | Dispositivo aparece en lista |
| T02 | Unitaria | Evitar duplicados | Error al agregar nombre existente |
| T03 | Integración | Conectar puerto serial | Estado "Conectado" |
| T04 | Integración | Encender LED | Log muestra OK |
| T05 | Integración | Control motor adelante | Motor gira en sentido horario |
| T06 | Integración | Control motor reversa | Motor gira en sentido antihorario |
| T07 | Integración | Detener motor | Motor se detiene |
| T08 | Integración | Leer temperatura | Valor numérico en °C |
| T09 | Sistema | Secuencia completa | Todos los comandos funcionan |
| T10 | Estrés | Comandos rápidos | Sin pérdida de datos |

### 8.2 Resultados de Pruebas

#### T01: Agregar Dispositivo ✅

**Procedimiento**:
1. Escribir "Lámpara 1" en el campo de texto
2. Hacer clic en "Agregar"
3. Verificar en ListBox

**Resultado**: EXITOSO
- Dispositivo aparece como "Lámpara 1 (On: False)"
- Log registra: "✓ Dispositivo 'Lámpara 1' agregado"

#### T02: Evitar Duplicados ✅

**Procedimiento**:
1. Agregar "Lámpara 1"
2. Intentar agregar "Lámpara 1" nuevamente
3. Verificar error

**Resultado**: EXITOSO
- ErrorProvider muestra: "Ya existe un dispositivo con ese nombre"
- El dispositivo no se agrega

#### T03: Conectar Puerto Serial ✅

**Procedimiento**:
1. Seleccionar puerto COM7
2. Hacer clic en "Conectar"
3. Verificar estado

**Resultado**: EXITOSO
- Label muestra: "Conectado a COM7"
- Log registra: "Conexión establecida en COM7"
- ESP32 envía "READY"

#### T04: Encender LED ✅

**Procedimiento**:
1. Seleccionar dispositivo
2. Hacer clic en "Encender"
3. Observar log y LED físico

**Resultado**: EXITOSO
- Log muestra: ">> LED1:ON" y "<< OK"
- LED integrado del ESP32 se enciende
- Estado del dispositivo cambia a (On: True)

#### T05-T07: Control de Motor ✅

**Procedimiento**:
1. Ajustar TrackBar a 200
2. Clic en "Adelante" → Motor gira horario
3. Clic en "Stop" → Motor se detiene
4. Ajustar TrackBar a 150
5. Clic en "Reversa" → Motor gira antihorario
6. Clic en "Stop" → Motor se detiene

**Resultado**: EXITOSO
- Log muestra secuencia completa de comandos y OK
- Motor responde correctamente a cada comando
- Velocidad es proporcional al valor del TrackBar

#### T08: Leer Temperatura ✅

**Procedimiento**:
1. Hacer clic en "Leer Temperatura"
2. Esperar respuesta
3. Verificar valor en label

**Resultado**: EXITOSO
- Log muestra: ">> TEMP?" y "<< TEMP:24.50"
- Label actualiza: "24.50 °C"
- Valor es consistente con temperatura ambiente

#### T09: Secuencia Completa ✅

**Procedimiento**:
1. Conectar
2. Crear 3 dispositivos
3. Encender 2, apagar 1
4. Controlar motor en ambas direcciones
5. Leer temperatura 3 veces
6. Desconectar

**Resultado**: EXITOSO
- Todas las operaciones ejecutadas sin errores
- Log completo y coherente
- Timestamps correctos en todos los eventos

#### T10: Comandos Rápidos ✅

**Procedimiento**:
1. Enviar 10 comandos LED1:ON/OFF alternados rápidamente
2. Verificar pérdida de datos

**Resultado**: EXITOSO
- Todos los comandos recibidos
- Todas las respuestas OK recibidas
- Sin errores de desbordamiento

### 8.3 Casos de Error Probados

#### Error: Sensor Desconectado ✅

**Procedimiento**: Desconectar LM75 y solicitar temperatura

**Resultado**: EXITOSO
- ESP32 responde: "ERR:TEMP"
- Aplicación maneja error sin crash

#### Error: Puerto No Abierto ✅

**Procedimiento**: Intentar enviar comando sin conexión

**Resultado**: EXITOSO
- Log muestra: "Puerto no abierto. No se pudo enviar el comando."
- Label de estado indica error

#### Error: Valor Fuera de Rango ✅

**Procedimiento**: Enviar comando `M1:SET:500`

**Resultado**: EXITOSO
- ESP32 responde: "ERR:VAL"
- Motor no se activa

---
### 9 Capturas

Las siguientes capturas documentan el funcionamiento:

1. **01_Creacion_Dispositivos.png**: Múltiples dispositivos creados
2. **02_Control_LED_Log.png**: Comunicación bidireccional funcionando
3. **03_Control_Motor.png**: Control PWM y dirección
4. **04_Lectura_Temperatura.png**: Sensor I2C operativo
5. **05_Log_Completo.png**: Registro completo de sesión
6. **06_Interfaz_Completa.png**: Vista general del sistema
7. **07_Manejo_Errores.png**: Validación y manejo de errores
8. **08_Hardware_ESP32.jpg**: Montaje físico del sistema

---
### 10 Librerías Utilizadas

| Librería | Versión | Uso |
|----------|---------|-----|
| System.IO.Ports | .NET 8.0 | Comunicación serial |
| Windows.Forms | .NET 8.0 | Interfaz gráfica |
| Wire.h | Arduino | Comunicación I2C |
| Arduino.h | Arduino | Framework ESP32 |
---

## Anexos

### Anexo A: Código Fuente Completo

El código fuente completo se encuentra en:
```
ControlDomotico/         # Aplicación C#
ESP32/PlatformIO/        # Firmware ESP32
```

### Anexo B: Esquemas de Conexión

Ver archivo: `Documentacion/Capturas/08_Hardware_ESP32.jpg`

### Anexo C: Protocolo de Comunicación Detallado

Ver archivo: `Documentacion/PROTOCOLO_COMUNICACION.md`

### Anexo D: Guía de Capturas

Ver archivo: `Documentacion/GUIA_CAPTURAS.md`

---

**Autor**: Juan Andres Gomez Ospina - Estudiante de Ingeniería Electrónica (4to Semestre)  l

---

## Notas Finales

Este documento ha sido creado para cumplir con los requisitos de documentación del proyecto de sistema domótico. Incluye todos los aspectos técnicos, arquitectura, implementación, pruebas y resultados obtenidos.

El sistema ha demostrado ser robusto, funcional y extensible, cumpliendo con todos los objetivos planteados. La documentación del protocolo de comunicación permite a futuros desarrolladores entender y extender el sistema fácilmente.

Para la generación del PDF, se recomienda convertir este archivo Markdown usando herramientas como:
- Pandoc con plantilla LaTeX
- Markdown to PDF en VSCode
- Typora con exportación a PDF
- Cualquier conversor online de Markdown a PDF

---

**Fin del documento**
