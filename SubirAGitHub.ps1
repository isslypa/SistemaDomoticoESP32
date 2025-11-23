# ========================================
# Script para Subir el Proyecto a GitHub
# ========================================

Write-Host ""
Write-Host "===============================================" -ForegroundColor Cyan
Write-Host "   Subir Proyecto a GitHub - Automatizado" -ForegroundColor Cyan
Write-Host "===============================================" -ForegroundColor Cyan
Write-Host ""

# Verificar si Git está instalado
try {
    $gitVersion = git --version
    Write-Host "✓ Git encontrado: $gitVersion" -ForegroundColor Green
} catch {
    Write-Host "✗ Error: Git no está instalado" -ForegroundColor Red
    Write-Host ""
    Write-Host "Para instalar Git:" -ForegroundColor Yellow
    Write-Host "  1. Con Winget: winget install --id Git.Git -e --source winget" -ForegroundColor White
    Write-Host "  2. Descargar desde: https://git-scm.com/download/win" -ForegroundColor White
    Write-Host ""
    Write-Host "Presiona cualquier tecla para salir..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 1
}

Write-Host ""

# Verificar configuración de Git
$userName = git config --global user.name
$userEmail = git config --global user.email

if ([string]::IsNullOrWhiteSpace($userName) -or [string]::IsNullOrWhiteSpace($userEmail)) {
    Write-Host "⚠️  Git no está configurado" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Por favor, configura Git con tu información:" -ForegroundColor Yellow
    Write-Host ""
    
    $name = Read-Host "Tu nombre completo"
    $email = Read-Host "Tu email"
    
    git config --global user.name "$name"
    git config --global user.email "$email"
    
    Write-Host ""
    Write-Host "✓ Git configurado correctamente" -ForegroundColor Green
} else {
    Write-Host "✓ Git configurado como: $userName <$userEmail>" -ForegroundColor Green
}

Write-Host ""

# Solicitar información del repositorio
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Información del Repositorio de GitHub" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Primero, crea un repositorio en GitHub:" -ForegroundColor Yellow
Write-Host "  1. Ve a: https://github.com/new" -ForegroundColor White
Write-Host "  2. Nombre: ControlDomotico (o el que prefieras)" -ForegroundColor White
Write-Host "  3. NO marques 'Add README', 'Add .gitignore' ni 'Add license'" -ForegroundColor White
Write-Host "  4. Clic en 'Create repository'" -ForegroundColor White
Write-Host ""

$usuario = Read-Host "Tu nombre de usuario de GitHub"
$repoName = Read-Host "Nombre del repositorio (Enter para 'ControlDomotico')"

if ([string]::IsNullOrWhiteSpace($repoName)) {
    $repoName = "ControlDomotico"
}

$repoUrl = "https://github.com/$usuario/$repoName.git"

Write-Host ""
Write-Host "Repositorio a usar: $repoUrl" -ForegroundColor Cyan
Write-Host ""

$confirm = Read-Host "¿Es correcto? (S/N)"
if ($confirm -ne "S" -and $confirm -ne "s") {
    Write-Host "Operación cancelada." -ForegroundColor Yellow
    Write-Host "Presiona cualquier tecla para salir..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 0
}

Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Preparando Repositorio Local" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Verificar si ya existe .git
if (Test-Path ".git") {
    Write-Host "⚠️  Ya existe un repositorio Git aquí" -ForegroundColor Yellow
    $reinit = Read-Host "¿Deseas reiniciar el repositorio? (S/N)"
    if ($reinit -eq "S" -or $reinit -eq "s") {
        Remove-Item -Recurse -Force .git
        Write-Host "✓ Repositorio anterior eliminado" -ForegroundColor Green
    }
}

# Inicializar repositorio
if (-not (Test-Path ".git")) {
    Write-Host "Inicializando repositorio Git..." -ForegroundColor Yellow
    git init
    Write-Host "✓ Repositorio inicializado" -ForegroundColor Green
}

Write-Host ""

# Agregar archivos
Write-Host "Agregando archivos al repositorio..." -ForegroundColor Yellow
git add .

Write-Host "✓ Archivos agregados" -ForegroundColor Green
Write-Host ""

# Mostrar estado
Write-Host "Archivos preparados:" -ForegroundColor Cyan
git status --short
Write-Host ""

# Hacer commit
Write-Host "Creando commit inicial..." -ForegroundColor Yellow
git commit -m "Initial commit: Sistema de control domótico ESP32 + C#"

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Commit creado exitosamente" -ForegroundColor Green
} else {
    Write-Host "✗ Error al crear el commit" -ForegroundColor Red
    Write-Host "Presiona cualquier tecla para salir..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 1
}

Write-Host ""

# Configurar remote
Write-Host "Configurando repositorio remoto..." -ForegroundColor Yellow

# Verificar si ya existe el remote
$existingRemote = git remote get-url origin 2>$null

if ($existingRemote) {
    Write-Host "⚠️  Ya existe un remote 'origin'" -ForegroundColor Yellow
    git remote remove origin
}

git remote add origin $repoUrl
Write-Host "✓ Remote configurado: $repoUrl" -ForegroundColor Green
Write-Host ""

# Cambiar a rama main
Write-Host "Configurando rama principal como 'main'..." -ForegroundColor Yellow
git branch -M main
Write-Host "✓ Rama configurada" -ForegroundColor Green
Write-Host ""

# Push a GitHub
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Subiendo a GitHub" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "⚠️  IMPORTANTE: Necesitarás autenticarte con GitHub" -ForegroundColor Yellow
Write-Host ""
Write-Host "Si te pide contraseña:" -ForegroundColor Yellow
Write-Host "  - NO uses tu contraseña de GitHub" -ForegroundColor White
Write-Host "  - Usa un Personal Access Token:" -ForegroundColor White
Write-Host "    1. Ve a: https://github.com/settings/tokens" -ForegroundColor White
Write-Host "    2. Generate new token (classic)" -ForegroundColor White
Write-Host "    3. Marca 'repo' (todos los permisos)" -ForegroundColor White
Write-Host "    4. Copia el token y pégalo cuando se te pida" -ForegroundColor White
Write-Host ""

$continue = Read-Host "¿Listo para subir? (S/N)"
if ($continue -ne "S" -and $continue -ne "s") {
    Write-Host "Operación cancelada." -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Puedes subir manualmente después con:" -ForegroundColor Cyan
    Write-Host "  git push -u origin main" -ForegroundColor White
    Write-Host ""
    Write-Host "Presiona cualquier tecla para salir..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 0
}

Write-Host ""
Write-Host "Subiendo archivos a GitHub..." -ForegroundColor Yellow
Write-Host ""

git push -u origin main

Write-Host ""

if ($LASTEXITCODE -eq 0) {
    Write-Host "================================================" -ForegroundColor Green
    Write-Host "  ✓ ¡PROYECTO SUBIDO EXITOSAMENTE!" -ForegroundColor Green
    Write-Host "================================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Tu proyecto está ahora en:" -ForegroundColor Cyan
    Write-Host "  https://github.com/$usuario/$repoName" -ForegroundColor White
    Write-Host ""
    Write-Host "Puedes:" -ForegroundColor Yellow
    Write-Host "  - Ver el código en línea" -ForegroundColor White
    Write-Host "  - Compartir el enlace" -ForegroundColor White
    Write-Host "  - Clonar en otras computadoras" -ForegroundColor White
    Write-Host "  - Colaborar con otros" -ForegroundColor White
    Write-Host ""
    
    $openBrowser = Read-Host "¿Deseas abrir el repositorio en el navegador? (S/N)"
    if ($openBrowser -eq "S" -or $openBrowser -eq "s") {
        Start-Process "https://github.com/$usuario/$repoName"
    }
} else {
    Write-Host "================================================" -ForegroundColor Red
    Write-Host "  ✗ Error al subir el proyecto" -ForegroundColor Red
    Write-Host "================================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Posibles causas:" -ForegroundColor Yellow
    Write-Host "  1. Error de autenticación (necesitas Personal Access Token)" -ForegroundColor White
    Write-Host "  2. El repositorio no existe en GitHub" -ForegroundColor White
    Write-Host "  3. Sin conexión a internet" -ForegroundColor White
    Write-Host ""
    Write-Host "Soluciones:" -ForegroundColor Yellow
    Write-Host "  1. Verifica que creaste el repo en: https://github.com/new" -ForegroundColor White
    Write-Host "  2. Configura autenticación: gh auth login" -ForegroundColor White
    Write-Host "  3. Intenta manualmente: git push -u origin main" -ForegroundColor White
    Write-Host ""
}

Write-Host "Presiona cualquier tecla para salir..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
