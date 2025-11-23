# Guía para Capturas de Pantalla del Sistema Domótico

Esta guía describe los escenarios que deben ser capturados para documentar el funcionamiento del sistema.

---

## 📸 Captura 1: Creación de Dispositivos

### Objetivo
Demostrar la capacidad de agregar múltiples dispositivos a la lista.

### Pasos para capturar:

1. **Iniciar la aplicación** de Windows Forms
2. **Agregar primer dispositivo**:
   - En el campo "Nombre del dispositivo", escribir: `Lámpara Sala`
   - Hacer clic en el botón **"Agregar"**
3. **Agregar segundo dispositivo**:
   - Escribir: `Lámpara Cocina`
   - Hacer clic en **"Agregar"**
4. **Agregar tercer dispositivo**:
   - Escribir: `Lámpara Dormitorio`
   - Hacer clic en **"Agregar"**

### Elementos que deben aparecer en la captura:

- ✅ Lista de dispositivos mostrando los 3 dispositivos creados
- ✅ Estado de cada dispositivo (On: False)
- ✅ Resumen en la parte inferior: `Total: 3 | Encendidos: 0 | Apagados: 3`
- ✅ Campo de texto vacío listo para agregar más dispositivos
- ✅ Log mostrando los mensajes: "✓ Dispositivo 'XXX' agregado"

### Nombre sugerido para la imagen: 
`01_Creacion_Dispositivos.png`

---

## 📸 Captura 2: Control de LED/Lámpara

### Objetivo
Mostrar el control de encendido/apagado de dispositivos y la comunicación serial.

### Pasos para capturar:

1. **Conectar el ESP32**:
   - Seleccionar el puerto COM en el ComboBox
   - Hacer clic en **"Conectar"**
   - Verificar que el label muestre: "Conectado a COMX"

2. **Encender un dispositivo**:
   - Seleccionar el primer dispositivo de la lista
   - Hacer clic en **"Encender"**
   - Observar el cambio de estado en la lista

3. **Alternar otro dispositivo**:
   - Seleccionar otro dispositivo
   - Hacer clic en **"Alternar"**

### Elementos que deben aparecer en la captura:

- ✅ Puerto COM seleccionado y estado "Conectado"
- ✅ Dispositivos con diferentes estados (algunos encendidos, otros apagados)
- ✅ Resumen actualizado mostrando encendidos/apagados
- ✅ **Log mostrando**:
  - Mensaje de conexión establecida
  - Comandos enviados: `>> LED1:ON`
  - Respuestas recibidas: `<< OK`
  - Timestamp de cada evento

### Nombre sugerido para la imagen:
`02_Control_LED_Log.png`

---

## 📸 Captura 3: Control del Motor DC

### Objetivo
Demostrar el control de velocidad y dirección del motor.

### Pasos para capturar:

1. **Motor adelante**:
   - Ajustar el TrackBar de velocidad a 180
   - Hacer clic en **"Adelante"**
   - Observar el log

2. **Motor en reversa**:
   - Ajustar el TrackBar a 150
   - Hacer clic en **"Reversa"**
   - Observar el log

3. **Detener motor**:
   - Hacer clic en **"Stop"**

### Elementos que deben aparecer en la captura:

- ✅ TrackBar de velocidad con un valor visible (ej: 150-200)
- ✅ Label mostrando: "Velocidad: 180" (o el valor seleccionado)
- ✅ Botones de control del motor visibles (Adelante, Reversa, Stop)
- ✅ **Log mostrando la secuencia completa**:
  ```
  >> M1:SET:180
  << OK
  Motor adelante a velocidad 180
  >> M1:SET:-150
  << OK
  Motor reversa a velocidad 150
  >> M1:STOP
  << OK
  Motor detenido
  ```

### Nombre sugerido para la imagen:
`03_Control_Motor.png`

---

## 📸 Captura 4: Lectura de Temperatura

### Objetivo
Mostrar la lectura del sensor LM75 y la actualización en tiempo real.

### Pasos para capturar:

1. **Solicitar temperatura**:
   - Hacer clic en el botón **"Leer Temperatura"**
   - Esperar la respuesta del ESP32

2. **Realizar múltiples lecturas**:
   - Hacer clic 3-4 veces más con intervalos de 2-3 segundos
   - Observar cómo se actualiza el valor de temperatura

### Elementos que deben aparecer en la captura:

- ✅ Label de temperatura mostrando el valor actual en formato: `XX.XX °C`
- ✅ Botón "Leer Temperatura" visible
- ✅ **Log mostrando múltiples lecturas**:
  ```
  Solicitando temperatura...
  >> TEMP?
  << TEMP:24.50
  Temperatura actualizada: 24.50 °C
  Solicitando temperatura...
  >> TEMP?
  << TEMP:24.50
  Temperatura actualizada: 24.50 °C
  Solicitando temperatura...
  >> TEMP?
  << TEMP:25.00
  Temperatura actualizada: 25.00 °C
  ```

### Nombre sugerido para la imagen:
`04_Lectura_Temperatura.png`

---

## 📸 Captura 5: Registro Completo de Eventos (Log Detallado)

### Objetivo
Mostrar una sesión completa de interacción con todos los comandos.

### Pasos para capturar:

Realizar en secuencia:
1. Conectar al puerto
2. Crear 2 dispositivos
3. Encender uno
4. Controlar el motor (adelante, stop)
5. Leer temperatura 2 veces
6. Apagar dispositivo

### Elementos que deben aparecer en la captura:

El log debe mostrar una secuencia completa como:

```
08:30:15 - Conexión establecida en COM7
08:30:15 - << READY
08:30:15 - << ESP32 DevKit V1 - Sistema Domotico
08:30:20 - ✓ Dispositivo 'Lámpara 1' agregado
08:30:25 - ✓ Dispositivo 'Lámpara 2' agregado
08:30:30 - >> LED1:ON
08:30:30 - << OK
08:30:35 - >> M1:SET:200
08:30:35 - << OK
08:30:35 - Motor adelante a velocidad 200
08:30:40 - >> M1:STOP
08:30:40 - << OK
08:30:40 - Motor detenido
08:30:45 - Solicitando temperatura...
08:30:45 - >> TEMP?
08:30:45 - << TEMP:23.50
08:30:45 - Temperatura actualizada: 23.50 °C
08:30:50 - >> LED1:OFF
08:30:50 - << OK
```

### Nombre sugerido para la imagen:
`05_Log_Completo.png`

---

## 📸 Captura 6: Interfaz Completa (Vista General)

### Objetivo
Mostrar todos los componentes de la interfaz en un solo vistazo.

### Elementos que deben aparecer:

- ✅ **Sección de Conexión Serial**:
  - ComboBox con puertos
  - Botones: Actualizar, Conectar, Desconectar
  - Estado de conexión visible

- ✅ **Sección de Dispositivos**:
  - Campo de texto para nombre
  - Botón Agregar
  - Lista de dispositivos
  - Botones: Encender, Apagar, Alternar
  - Botones: Encender Todos, Apagar Todos
  - Resumen (Total, Encendidos, Apagados)

- ✅ **Sección de Control de Motor**:
  - TrackBar de velocidad
  - Label de velocidad actual
  - Botones: Adelante, Reversa, Stop

- ✅ **Sección de Temperatura**:
  - Botón Leer Temperatura
  - Label mostrando temperatura actual

- ✅ **Log de eventos**:
  - ListBox con varios eventos registrados

### Nombre sugerido para la imagen:
`06_Interfaz_Completa.png`

---

## 📸 Captura 7 (BONUS): Manejo de Errores

### Objetivo
Documentar cómo el sistema maneja situaciones de error.

### Escenarios a capturar:

1. **Error de duplicado**:
   - Intentar agregar un dispositivo con nombre existente
   - Mostrar el ErrorProvider en el campo de texto

2. **Error de comunicación**:
   - Con el ESP32 desconectado, intentar enviar comandos
   - Mostrar mensajes de error en el log

### Elementos esperados:

- ✅ ErrorProvider mostrando mensaje: "Ya existe un dispositivo con ese nombre"
- ✅ Log mostrando: "Puerto no abierto. No se pudo enviar el comando."

### Nombre sugerido para la imagen:
`07_Manejo_Errores.png`

---

## 📸 Captura 8 (BONUS): Hardware ESP32

### Objetivo
Mostrar el montaje físico del sistema.

### Elementos a fotografiar:

1. **ESP32 DevKit V1** conectado por USB
2. **Módulo L298N** con motor DC conectado
3. **Sensor LM75** conectado a pines I2C
4. **LED** (puede ser el integrado o uno externo)
5. **Cables de conexión** visibles

### Etiquetas recomendadas en la imagen:

- GPIO2 → LED
- GPIO23, 19, 18 → L298N
- GPIO21, 22 → LM75 (SDA, SCL)

### Nombre sugerido para la imagen:
`08_Hardware_ESP32.png`

---
## 📂 Estructura de Carpetas Sugerida

```
Documentacion/
├── Capturas/
│   ├── 01_Creacion_Dispositivos.png
│   ├── 02_Control_LED_Log.png
│   ├── 03_Control_Motor.png
│   ├── 04_Lectura_Temperatura.png
│   ├── 05_Log_Completo.png
│   ├── 06_Interfaz_Completa.png
│   ├── 07_Manejo_Errores.png
│   └── 08_Hardware_ESP32.jpg
├── PROTOCOLO_COMUNICACION.md
├── GUIA_CAPTURAS.md
└── DocumentacionCompleta.pdf
```

---

## ✅ Checklist Final

Antes de considerar completa la documentación visual, verificar:

- [ ] Todas las capturas están en buena calidad
- [ ] El log es legible en todas las imágenes
- [ ] Se muestran comandos y respuestas claramente
- [ ] Los timestamps son visibles
- [ ] La interfaz completa se ve profesional
- [ ] El hardware está bien fotografiado y etiquetado
- [ ] Todas las imágenes están nombradas correctamente
- [ ] Las imágenes están organizadas en la carpeta correcta

---