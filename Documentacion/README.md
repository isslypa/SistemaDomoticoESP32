# 📚 Documentación del Sistema de Control Domótico

Esta carpeta contiene toda la documentación técnica del proyecto de Sistema de Control Domótico con ESP32 y C#.

---

## 📁 Archivos Disponibles

### 📄 [PROTOCOLO_COMUNICACION.md](PROTOCOLO_COMUNICACION.md)
**Protocolo de Comunicación Serial Completo**

- ✅ Configuración de comunicación (115200 bps)
- ✅ **Comandos utilizados**: LED1:ON/OFF, M1:SET, M1:STOP, TEMP?, PING, STATE?
- ✅ **Respuestas esperadas**: OK, ERR, TEMP:XX.XX, PONG, STATE:...
- ✅ **Ejemplos de interacción**: 4 escenarios completos
- ✅ Códigos de error y su manejo
- ✅ Diagramas de flujo
- ✅ Implementación en C# y Arduino

**📄 Páginas**: ~8  
**🎯 Uso**: Referencia técnica del protocolo

---

### 📘 [DOCUMENTACION_COMPLETA.md](DOCUMENTACION_COMPLETA.md)
**Documento Técnico Principal del Proyecto**

**Contenido (11 secciones)**:
1. Introducción y objetivos
2. Descripción del sistema
3. Arquitectura completa (4 capas)
4. Componentes de software (C# + ESP32)
5. Componentes de hardware
6. Protocolo de comunicación (resumen)
7. Implementación (código explicado)
8. Pruebas realizadas (10 casos)
9. Resultados y métricas
10. Conclusiones
11. Referencias

**📄 Páginas**: ~25  
**🎯 Uso**: Documento principal para entrega (convertir a PDF)

---

### 📸 [GUIA_CAPTURAS.md](GUIA_CAPTURAS.md)
**Guía para Tomar Capturas de Pantalla**

- ✅ 8 escenarios de captura detallados
- ✅ Pasos exactos para cada captura
- ✅ Elementos que deben aparecer
- ✅ Nombres sugeridos para archivos
- ✅ Recomendaciones de calidad
- ✅ Checklist de validación

**📄 Páginas**: ~6  
**🎯 Uso**: Seguir para documentar el funcionamiento

---

### 🌐 [GUIA_GITHUB.md](GUIA_GITHUB.md)
**Guía para Subir el Proyecto a GitHub**

- ✅ Requisitos previos
- ✅ Pasos detallados para subir
- ✅ Configuración de Git
- ✅ Autenticación en GitHub
- ✅ Comandos útiles
- ✅ Solución de problemas

**📄 Páginas**: ~7  
**🎯 Uso**: Subir proyecto a GitHub para compartirlo

---

### 📂 Capturas/
**Carpeta para Imágenes del Sistema**

Capturas requeridas:
1. `01_Creacion_Dispositivos.png` - Crear 3 dispositivos
2. `02_Control_LED_Log.png` - Comunicación serial
3. `03_Control_Motor.png` - Control de motor
4. `04_Lectura_Temperatura.png` - Sensor de temperatura
5. `05_Log_Completo.png` - Sesión completa
6. `06_Interfaz_Completa.png` - Vista general
7. `07_Manejo_Errores.png` - Validaciones (opcional)
8. `08_Hardware_ESP32.jpg` - Montaje físico (opcional)

---

## 🎯 Entregables del Proyecto

### ✅ Requisito 2: Protocolo de Comunicación
**Estado**: COMPLETADO

Archivo: [PROTOCOLO_COMUNICACION.md](PROTOCOLO_COMUNICACION.md)

- [x] Comandos utilizados
- [x] Respuestas esperadas
- [x] Ejemplos de interacción

---

### ⏳ Requisito 3: Capturas de Funcionamiento
**Estado**: PENDIENTE (Tomar capturas)

Archivo: [GUIA_CAPTURAS.md](GUIA_CAPTURAS.md)

- [ ] Creación de dispositivos
- [ ] Control del motor
- [ ] Lectura de temperatura
- [ ] Registro de eventos en el log

---

### ⏳ Requisito 4: Archivo PDF
**Estado**: LISTO PARA CONVERTIR

Archivo: [DOCUMENTACION_COMPLETA.md](DOCUMENTACION_COMPLETA.md)

- [x] Descripción del sistema
- [x] Arquitectura
- [x] Pruebas realizadas
- [ ] Conversión a PDF (pendiente)

---

## 🚀 Inicio Rápido

### 1️⃣ Tomar Capturas (15 min)
```powershell
code GUIA_CAPTURAS.md
```
Seguir las instrucciones para capturar el funcionamiento del sistema.

### 2️⃣ Generar PDF (5 min)

**Opción A - VSCode**:
1. Instalar extensión "Markdown PDF"
2. Abrir `DOCUMENTACION_COMPLETA.md`
3. `Ctrl+Shift+P` → "Markdown PDF: Export (pdf)"

**Opción B - Online**:
1. Ir a https://www.markdowntopdf.com/
2. Subir `DOCUMENTACION_COMPLETA.md`
3. Descargar el PDF

### 3️⃣ Subir a GitHub (10 min)
```powershell
# Desde la raíz del proyecto (3_C)
cd ..
.\SubirAGitHub.ps1
```
O seguir la guía manual: [GUIA_GITHUB.md](GUIA_GITHUB.md)

---

## 📊 Estado de Documentación

| Documento | Estado | Completado |
|-----------|--------|------------|
| Protocolo de Comunicación | ✅ Listo | 100% |
| Guía de Capturas | ✅ Listo | 100% |
| Documentación Completa | ✅ Listo | 100% |
| Guía GitHub | ✅ Listo | 100% |
| Capturas de Pantalla | ⏳ Pendiente | 0% |
| Conversión a PDF | ⏳ Pendiente | 0% |
| Subida a GitHub | ⏳ Pendiente | 0% |

**Progreso Total**: 60%

---

## 🔧 Herramientas Necesarias

### Para Capturas:
- Windows Snipping Tool (`Win + Shift + S`)
- Aplicación C# ejecutándose
- ESP32 conectado

### Para PDF:
- VSCode + Markdown PDF (recomendado)
- O cualquier conversor online
- O Pandoc (avanzado)

### Para GitHub:
- Git instalado
- Cuenta de GitHub
- Personal Access Token

---

## 📝 Comandos Rápidos

### Ver todos los archivos:
```powershell
Get-ChildItem -Recurse
```

### Abrir documentos:
```powershell
code PROTOCOLO_COMUNICACION.md
code DOCUMENTACION_COMPLETA.md
code GUIA_CAPTURAS.md
code GUIA_GITHUB.md
```

### Generar PDF (con Pandoc):
```powershell
pandoc DOCUMENTACION_COMPLETA.md -o DocumentacionCompleta.pdf --toc --toc-depth=3
```

### Subir a GitHub:
```powershell
cd ..
.\SubirAGitHub.ps1
```

---

## 🌐 Ver en GitHub

Una vez subido el proyecto, toda esta documentación se verá con formato en:
```
https://github.com/TU_USUARIO/ControlDomotico/tree/main/Documentacion
```

GitHub renderizará automáticamente:
- ✅ Todos los archivos `.md` con formato
- ✅ Tablas estructuradas
- ✅ Código con sintaxis resaltada
- ✅ Enlaces internos funcionando
- ✅ Imágenes (cuando las agregues)

---

## 💡 Consejos

### Para las Capturas:
- Asegúrate de que el log sea legible
- Captura toda la interfaz cuando sea relevante
- Los timestamps deben ser visibles
- Toma más capturas de las necesarias

### Para el PDF:
- Verifica que tenga tabla de contenidos
- Revisa que las tablas se vean bien
- Asegúrate de que el código tenga formato

### Para GitHub:
- Usa el script `SubirAGitHub.ps1` para automatizar
- Sigue la [GUIA_GITHUB.md](GUIA_GITHUB.md) si es manual
- Las capturas se pueden agregar después

---

## 📞 Ayuda

¿Necesitas ayuda con algún paso?

- **Protocolo**: Ya está completo, solo léelo
- **Capturas**: Sigue [GUIA_CAPTURAS.md](GUIA_CAPTURAS.md)
- **PDF**: Usa VSCode + Markdown PDF (más fácil)
- **GitHub**: Ejecuta `SubirAGitHub.ps1` o lee [GUIA_GITHUB.md](GUIA_GITHUB.md)

---

## ✅ Checklist Final

Antes de entregar, verifica:

- [x] PROTOCOLO_COMUNICACION.md completo
- [x] DOCUMENTACION_COMPLETA.md completo
- [x] GUIA_CAPTURAS.md completo
- [x] GUIA_GITHUB.md completo
- [ ] Capturas tomadas (6-8 imágenes)
- [ ] PDF generado
- [ ] Proyecto subido a GitHub (opcional pero recomendado)

---

**Última actualización**: 23 de Noviembre de 2025  
**Versión**: 2.0 (con soporte para GitHub)  
**Estado**: Listo para capturas, PDF y GitHub

---

¡La documentación está completa y lista para compartir! 🎉
