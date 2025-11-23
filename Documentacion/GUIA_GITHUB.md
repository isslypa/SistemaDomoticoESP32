# 🚀 Guía para Subir el Proyecto a GitHub

Esta guía te ayudará a subir todo el proyecto a GitHub paso a paso.

---

## 📋 Requisitos Previos

1. **Cuenta de GitHub**
   - Si no tienes, créala en: https://github.com/signup

2. **Git instalado**
   - Verificar si está instalado:
     ```powershell
     git --version
     ```
   - Si no está instalado:
     - Con Winget: `winget install --id Git.Git -e --source winget`
     - O descargar desde: https://git-scm.com/download/win

3. **Configurar Git** (primera vez):
   ```powershell
   git config --global user.name "Tu Nombre"
   git config --global user.email "tu_email@ejemplo.com"
   ```

---

## 🎯 Pasos Rápidos

### 1️⃣ Crear Repositorio en GitHub (Web)

1. Ir a: https://github.com/new
2. Configurar:
   - **Repository name**: `ControlDomotico` o `SistemaDomoticoESP32`
   - **Description**: `Sistema de control domótico con ESP32 y C# Windows Forms`
   - **Public** o **Private** (tu elección)
   - ✅ **NO** marcar "Add a README file" (ya tenemos uno)
   - ✅ **NO** agregar .gitignore (ya tenemos uno)
   - ✅ **NO** agregar licencia (ya tenemos una)
3. Clic en **"Create repository"**

### 2️⃣ Subir el Proyecto desde PowerShell

```powershell
# Navegar a la carpeta del proyecto
cd C:\Users\Juan\Progra\3_C

# Inicializar repositorio Git
git init

# Agregar todos los archivos
git add .

# Hacer el primer commit
git commit -m "Initial commit: Sistema de control domótico ESP32 + C#"

# Agregar el repositorio remoto (REEMPLAZAR con tu URL)
git remote add origin https://github.com/TU_USUARIO/ControlDomotico.git

# Cambiar a rama main
git branch -M main

# Subir todo a GitHub
git push -u origin main
```

**Importante**: Reemplaza `TU_USUARIO` con tu nombre de usuario de GitHub.

---

## 📝 Paso a Paso Detallado

### Paso 1: Navegar a tu proyecto

```powershell
cd C:\Users\Juan\Progra\3_C
```

### Paso 2: Inicializar Git

```powershell
git init
```

Esto creará una carpeta `.git` oculta en tu proyecto.

### Paso 3: Ver qué archivos se subirán

```powershell
git status
```

Deberías ver todos tus archivos en verde (listos para agregar).

### Paso 4: Agregar archivos

```powershell
# Agregar todos los archivos
git add .

# O agregar archivos específicos
git add README.md
git add Documentacion/
git add ControlDomotico/
git add ESP32/
```

### Paso 5: Hacer commit

```powershell
git commit -m "Initial commit: Sistema de control domótico ESP32 + C#"
```

### Paso 6: Conectar con GitHub

```powershell
# Reemplaza TU_USUARIO con tu nombre de usuario de GitHub
git remote add origin https://github.com/TU_USUARIO/ControlDomotico.git
```

### Paso 7: Cambiar a rama main

```powershell
git branch -M main
```

### Paso 8: Subir a GitHub

```powershell
git push -u origin main
```

**Nota**: La primera vez te pedirá autenticarte con GitHub:
- Usar **Personal Access Token** (recomendado)
- O configurar **SSH keys**

---

## 🔐 Autenticación en GitHub

### Opción A: Personal Access Token (Recomendado)

1. Ir a: https://github.com/settings/tokens
2. Clic en **"Generate new token (classic)"**
3. Dar permisos: `repo` (todos los permisos de repositorio)
4. Copiar el token generado
5. Cuando Git pida contraseña, pegar el token

### Opción B: GitHub CLI

```powershell
# Instalar GitHub CLI
winget install --id GitHub.cli

# Autenticarse
gh auth login

# Seguir las instrucciones en pantalla
```

---

## 📂 Estructura que se Subirá

```
ControlDomotico/
├── README.md                    ⬆️ Página principal del repo
├── LICENSE                      ⬆️ Licencia MIT
├── .gitignore                   ⬆️ Archivos ignorados
├── ControlDomotico/             ⬆️ Código C#
│   ├── Program.cs
│   ├── Domain/
│   ├── UI/
│   └── ControlDomotico.csproj
├── ESP32/                       ⬆️ Firmware ESP32
│   └── PlatformIO/
│       └── Projects/
│           └── Final/
│               ├── platformio.ini
│               └── src/
│                   └── main.cpp
└── Documentacion/               ⬆️ Toda la documentación
    ├── PROTOCOLO_COMUNICACION.md
    ├── DOCUMENTACION_COMPLETA.md
    ├── GUIA_CAPTURAS.md
    ├── README.md
    ├── COMO_GENERAR_PDF.md
    ├── RESUMEN_ENTREGABLES.md
    ├── INICIO_RAPIDO.md
    └── Capturas/
        └── README_CAPTURAS.md
```

**Nota**: Los archivos compilados (`bin/`, `obj/`, `.vs/`) NO se subirán gracias al `.gitignore`.

---

## ✨ Cómo se Verá en GitHub

### Página Principal

- ✅ **README.md** se mostrará automáticamente con formato
- ✅ **Badges** coloridos (tecnologías, estado)
- ✅ **Tabla de contenidos** navegable
- ✅ **Diagramas ASCII** formateados
- ✅ **Tablas** bien estructuradas
- ✅ **Código con sintaxis resaltada**

### Carpeta Documentacion/

- ✅ Todos los `.md` se renderizarán con formato
- ✅ Enlaces internos funcionarán
- ✅ Tablas y diagramas se verán perfectos
- ✅ Código con colores

### Navegación

Los usuarios podrán:
- Ver todo el código fuente
- Leer la documentación con formato
- Descargar el proyecto completo
- Clonar el repositorio
- Hacer fork para colaborar

---

## 🎨 Mejorar la Presentación (Opcional)

### Agregar Capturas Reales

Cuando tomes las capturas:

```powershell
# Agregar nuevas capturas
git add Documentacion/Capturas/*.png

# Commit
git commit -m "Agregar capturas del sistema funcionando"

# Subir
git push
```

### Actualizar el README

Si quieres cambiar algo en el README principal:

```powershell
# Editar el archivo
code README.md

# Guardar cambios
git add README.md
git commit -m "Actualizar README con capturas reales"
git push
```

---

## 🔄 Comandos Git Útiles

### Ver estado actual
```powershell
git status
```

### Ver historial de commits
```powershell
git log --oneline
```

### Agregar archivos nuevos
```powershell
git add archivo.txt
git add carpeta/
git add .
```

### Hacer commit
```powershell
git commit -m "Mensaje descriptivo"
```

### Subir cambios
```powershell
git push
```

### Ver archivos ignorados
```powershell
git status --ignored
```

---

## 📸 Agregar Capturas Después

Si aún no tienes las capturas:

1. Sube el proyecto ahora sin las capturas
2. Toma las capturas siguiendo `GUIA_CAPTURAS.md`
3. Agrégalas después:

```powershell
# Copiar capturas a la carpeta
Copy-Item *.png Documentacion\Capturas\

# Agregar a Git
git add Documentacion/Capturas/*.png

# Commit
git commit -m "Agregar capturas del funcionamiento del sistema"

# Subir
git push
```

---

## 🌐 Compartir tu Proyecto

Una vez subido, tu proyecto estará en:
```
https://github.com/TU_USUARIO/ControlDomotico
```

Podrás compartir:
- 📎 **URL del repo**: Para que otros lo vean y descarguen
- 📋 **Documentación**: Directamente desde GitHub (se ve con formato)
- 📂 **Código**: Navegable con sintaxis resaltada
- 🐛 **Issues**: Para reportar problemas
- 🤝 **Pull Requests**: Para colaboraciones

---

## 🆘 Solución de Problemas

### Error: "remote origin already exists"

```powershell
git remote remove origin
git remote add origin https://github.com/TU_USUARIO/ControlDomotico.git
```

### Error: "failed to push"

```powershell
# Forzar push (solo si estás seguro)
git push -u origin main --force
```

### Error: Autenticación fallida

- Usar Personal Access Token en lugar de contraseña
- O instalar GitHub CLI: `gh auth login`

### Ver qué remote tienes configurado

```powershell
git remote -v
```

---

## ✅ Checklist Final

Antes de subir, verifica:

- [ ] Git está instalado (`git --version`)
- [ ] Creaste el repositorio en GitHub
- [ ] Configuraste tu nombre y email en Git
- [ ] Estás en la carpeta correcta (`C:\Users\Juan\Progra\3_C`)
- [ ] Tienes conexión a internet
- [ ] Conoces tu usuario de GitHub

Después de subir, verifica:

- [ ] El repositorio se ve en `https://github.com/TU_USUARIO/ControlDomotico`
- [ ] El README.md se muestra con formato en la página principal
- [ ] La carpeta Documentacion/ es navegable
- [ ] Los archivos .md se ven con formato
- [ ] No se subieron archivos compilados (bin/, obj/)

---

## 🎉 ¡Listo!

Tu proyecto ahora está en GitHub y se verá profesional. GitHub automáticamente:

- ✅ Renderiza todos los archivos `.md` con formato
- ✅ Muestra el README.md en la página principal
- ✅ Permite navegar por carpetas
- ✅ Resalta la sintaxis del código
- ✅ Permite clonar y descargar el proyecto

---

## 📚 Recursos Adicionales

- **Git Cheat Sheet**: https://education.github.com/git-cheat-sheet-education.pdf
- **GitHub Docs**: https://docs.github.com/
- **Markdown Guide**: https://www.markdownguide.org/
- **Git Tutorial**: https://git-scm.com/docs/gittutorial

---

**¿Necesitas ayuda?** Abre un issue en tu repositorio o consulta la documentación de Git.

¡Éxito con tu proyecto en GitHub! 🚀
