# Protocolo de Comunicación Serial - Sistema Domótico

## Configuración de Comunicación

- **Puerto Serial**: COM variable (detectado automáticamente)
- **Velocidad (Baud Rate)**: 115200 bps
- **Formato**: Comandos en texto ASCII plano

---

## Tabla de Comandos

### 1. Comandos de LED/Lámpara

| Comando | Descripción | Respuesta Esperada | Observaciones |
|---------|-------------|-------------------|---------------|
| `LED1:ON` | Enciende el LED1 (GPIO2) | `OK` | Activa la salida digital |
| `LED1:OFF` | Apaga el LED1 (GPIO2) | `OK` | Desactiva la salida digital |

**Ejemplo de uso:**
```
>> LED1:ON
<< OK

>> LED1:OFF
<< OK
```

---

### 2. Comandos de Motor DC

| Comando | Descripción | Respuesta Esperada | Observaciones |
|---------|-------------|-------------------|---------------|
| `M1:SET:<valor>` | Establece velocidad y dirección del motor | `OK` o `ERR:VAL` | Valor: -255 a 255 |
| `M1:STOP` | Detiene el motor | `OK` | Velocidad = 0 |

**Parámetros para M1:SET:**
- **Valor positivo (1 a 255)**: Motor adelante
- **Valor negativo (-1 a -255)**: Motor reversa
- **Valor 0**: Motor detenido
- **Fuera de rango**: Error `ERR:VAL`

**Ejemplos de uso:**
```
>> M1:SET:150
<< OK
(Motor avanza a velocidad media)

>> M1:SET:-200
<< OK
(Motor retrocede a velocidad alta)

>> M1:STOP
<< OK
(Motor se detiene)

>> M1:SET:300
<< ERR:VAL
(Valor fuera de rango)
```

---

### 3. Comandos de Sensor de Temperatura (LM75)

| Comando | Descripción | Respuesta Esperada | Observaciones |
|---------|-------------|-------------------|---------------|
| `TEMP?` | Solicita lectura de temperatura | `TEMP:<valor>` o `ERR:TEMP` | Valor en grados Celsius |

**Formato de respuesta exitosa:**
```
TEMP:25.50
```
(Temperatura de 25.50 °C)

**Ejemplo de uso:**
```
>> TEMP?
<< TEMP:23.50

>> TEMP?
<< ERR:TEMP
(Error de comunicación I2C)
```

---

### 4. Comandos de Diagnóstico

| Comando | Descripción | Respuesta Esperada | Observaciones |
|---------|-------------|-------------------|---------------|
| `PING` | Verifica conectividad | `PONG` | Prueba de comunicación |
| `STATE?` | Consulta estado de dispositivos | `STATE:LED1=<0|1>,M1=<-255..255>` | Estado completo |

**Ejemplos de uso:**
```
>> PING
<< PONG

>> STATE?
<< STATE:LED1=1,M1=150
(LED encendido, motor a 150)
```

---

## Mensajes de Error

| Código | Significado | Causa |
|--------|-------------|-------|
| `ERR` | Comando no reconocido | Sintaxis incorrecta o comando inexistente |
| `ERR:VAL` | Valor fuera de rango | Parámetro numérico inválido |
| `ERR:TEMP` | Error en sensor de temperatura | Fallo en comunicación I2C con LM75 |
| `ERR:OVERFLOW` | Desbordamiento de buffer | Comando demasiado largo (>80 caracteres) |

---
## Ejemplos de Interacción Completa

### Escenario 1: Encender lámpara y verificar

```
PC: LED1:ON
ESP32: OK

PC: STATE?
ESP32: STATE:LED1=1,M1=0
```

### Escenario 2: Control de motor - adelante y reversa

```
PC: M1:SET:180
ESP32: OK
(Motor avanza a velocidad 180)

(después de 3 segundos)

PC: M1:STOP
ESP32: OK

PC: M1:SET:-150
ESP32: OK
(Motor retrocede a velocidad 150)

PC: M1:STOP
ESP32: OK
```

### Escenario 3: Lectura periódica de temperatura

```
PC: TEMP?
ESP32: TEMP:24.00

(espera 2 segundos)

PC: TEMP?
ESP32: TEMP:24.50

(espera 2 segundos)

PC: TEMP?
ESP32: TEMP:25.00
```

### Escenario 4: Manejo de errores

```
PC: LED2:ON
ESP32: ERR
(Comando no reconocido)

PC: M1:SET:500
ESP32: ERR:VAL
(Valor fuera de rango -255 a 255)

PC: TEMP?
ESP32: ERR:TEMP
(Sensor desconectado o mal configurado)
```

---

## Implementación en C#

### Envío de comandos

```csharp
private void EnviarComando(string comando)
{
    if (!_sp.IsOpen) return;
    
    _sp.WriteLine(comando);  // Agrega \n automáticamente
    Log($">> {comando}");
}
```

### Recepción de respuestas

```csharp
private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
{
    string linea = _sp.ReadLine();
    string limpio = linea.Trim();
    
    Log($"<< {limpio}");
    
    // Procesamiento específico
    if (limpio.StartsWith("TEMP:"))
    {
        string tempStr = limpio.Substring(5).Trim();
        if (float.TryParse(tempStr, out float temperatura))
        {
            lblTemperaturaValor.Text = $"{temperatura:F2} °C";
        }
    }
}
```

---

## Implementación en ESP32

### Procesamiento de comandos

```cpp
void processCommand(const String& cmd) {
    String c = cmd;
    c.trim();
    
    if (c == "LED1:ON") {
        setLed1(true);
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

## Diagrama de Flujo de Comunicación

```
┌─────────────┐                    ┌──────────────┐
│   C# App    │                    │   ESP32      │
│  (Windows)  │                    │  (DevKit V1) │
└──────┬──────┘                    └──────┬───────┘
       │                                  │
       │──── Conectar a 115200 bps ──────>│
       │                                  │
       │<────── READY ───────────────────│
       │<────── ESP32 DevKit V1... ──────│
       │                                  │
       │──── LED1:ON ────────────────────>│
       │                                  │ [Enciende GPIO2]
       │<────── OK ──────────────────────│
       │                                  │
       │──── TEMP? ──────────────────────>│
       │                                  │ [Lee I2C/LM75]
       │<────── TEMP:24.50 ──────────────│
       │                                  │
       │──── M1:SET:200 ─────────────────>│
       │                                  │ [PWM + Dirección]
       │<────── OK ──────────────────────│
       │                                  │
```

---

## Notas Técnicas

### Hardware ESP32
- **Pin LED1**: GPIO2 (LED integrado)
- **Pin Motor ENA**: GPIO23 (PWM)
- **Pin Motor IN1**: GPIO19
- **Pin Motor IN2**: GPIO18
- **Pin I2C SDA**: GPIO21
- **Pin I2C SCL**: GPIO22

### Configuración PWM Motor
- **Canal**: 0
- **Frecuencia**: 10 kHz
- **Resolución**: 8 bits (0-255)

### Sensor LM75
- **Dirección I2C**: 0x48 (A0,A1,A2 SOLDADOS A GND)
- **Resolución**: 0.5 °C
- **Rango típico**: -55 °C a +125 °C
- **Registro de temperatura**: 0x00

---

## Pruebas Realizadas

1. **Test de conectividad**: Enviar `PING`, esperar `PONG`
2. **Test de LED**: Alternar `LED1:ON` / `LED1:OFF`
3. **Test de motor**: 
   - Adelante: `M1:SET:150`
   - Parar: `M1:STOP`
   - Reversa: `M1:SET:-150`
4. **Test de temperatura**: Enviar `TEMP?` múltiples veces
5. **Test de errores**: Enviar comandos inválidos y verificar manejo

---