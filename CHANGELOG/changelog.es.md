# Changelog — Sailock
Todos los cambios de Sailock se documentan aquí.

## [1.5.0] - 2026-07-16

### Añadido
* Nuevo asistente de importación con los modos Combinar, Reemplazar y Personalizado
* Gestión configurable de duplicados durante la importación (Mantener existente, Sobrescribir o Renombrar)
* Nuevas columnas Fecha de creación y Fecha de modificación en el Panel principal
* Localización completa de todos los valores desplegables de Configuración (inglés, español, alemán y francés)

### Cambiado
* Flujo de importación rediseñado con confirmación antes de aplicar los cambios
* El Panel principal ahora utiliza un menú contextual de tres puntos en lugar del botón Editar
* Mejorado el diseño del Panel principal y el formato de fecha
* La pantalla de inicio de sesión ahora siempre comienza con el tema oscuro y aplica el tema seleccionado tras desbloquear la bóveda
* Los textos de la interfaz ahora utilizan mayúsculas y minúsculas de forma coherente
* Eliminada la vista de Registro de cambios integrada; las notas de versión ahora están disponibles en el repositorio de GitHub

### Corregido
* Colores más suaves en el tema claro para mejorar la legibilidad
* Corregida la visibilidad de la etiqueta de Bloqueo automático en el tema claro
* Corregido el color del texto descriptivo de Bloqueo automático en Configuración
* Corregidas las traducciones faltantes en el cuadro de diálogo Cambiar contraseña maestra
* Corregida la persistencia del tema al iniciar la aplicación

---

## [1.4.2] - 2026-07-01

### Fixed
* Corregido el problema de memoria caché al actualizar la contraseña maestra

---

## [1.4.1] - 2026-06-28

### Fixed
* Añadida la traducción para Inglés, Español, Alemán y Francés en el modal de Change Master Password

---

## [1.4.0] - 2026-06-21

### Added
* Nuevo filtro de categorías en Dashboard para visualizar entradas por categoría o todas las entradas.
* Campo URL/Website en las entradas de contraseña para identificar cuentas más fácilmente.
* Soporte para idioma Alemán (Deutsch).
* Soporte para idioma Francés (Français).

### Changed
* Dashboard ahora muestra una columna Website con la URL asociada a cada entrada.
* La búsqueda ahora incluye coincidencias en URLs y sitios web.
* La sección de idioma en Ajustes incluye nuevas opciones de localización.

---

## [1.3.1] - 2026-06-15

### Added
* Tiempo de bloqueo automático configurable: 30 segundos, 1 minuto, 2 minutos o 5 minutos, o desactivado por completo

### Changed
* El selector de Auto-Lock en Ajustes ahora es un desplegable, igual que el de idioma o tamaño de texto
* La descripción de Auto-Lock en Ajustes ahora es neutral

---

## [1.3.0] - 2026-06-08

### Added
* Tema claro completo implementado en toda la aplicación
* Cambio de contraseña maestra funcional con validaciones de seguridad
* Barras de desplazamiento unificadas en toda la app con estilo consistente
* Hover mejorado en botones de ventana (minimizar, maximizar, cerrar)

### Changed
* Barras de scroll ahora tienen el mismo estilo visual en Dashboard, Settings, Generator y Changelog
* Interfaz Settings con ScrollViewer para mejor navegación en pantallas pequeñas
* Sidebar mejorado con hover más visible en modo claro
* Badge Latest/Legacy en Changelog con colores personalizables
* ScrollViewer en Dashboard ahora fuera de la tabla para mejor experiencia

### Fixed
* Textos blancos en modo claro ahora son visibles (negro/verde oscuro)
* Checkboxes y toggles ahora visibles en modo claro
* Botones de ventana con hover más visible en ambos temas
* ScrollBar en Generator ahora aparece cuando la ventana es pequeña
* Badges en Changelog ahora con contraste adecuado

---

## [1.2.2] - 2026-06-05

### Fixed
* Restaurada la visibilidad del botón de versión en la barra lateral
* Corregido un problema que hacía el acceso al historial de versiones poco visible

---

## [1.2.1] - 2026-06-04

### Fixed
* Corregida la desincronización entre campo de contraseña visible y oculto
* Solucionado el caché visual en el campo de contraseña maestra
* Corregida la alineación del campo de contraseña al alternar visibilidad
* Corregidos errores ortográficos en textos en español

### Changed
* Fuente actualizada a JetBrains Mono
* Ajustados los tamaños Small, Default y Large con mayor diferencia
* El escalado no afecta a modales ni formularios
* La barra lateral escala junto al contenido

---

## [1.2.0] - 2026-06-01

### Added
* Añadido soporte completo a la aplicación para: Inglés y Español
* La selección de idioma ya está disponible en Ajustes y se guarda entre sesiones
* Todas las vistas, ventanas modales y mensajes del sistema han sido traducidos completamente

### Changed
* Mejorado el estilo de las barras de desplazamiento en la vista de Historial de cambios
* El botón de versión de la barra lateral ahora utiliza un estilo coherente con la pantalla de inicio de sesión

### Fixed
* Corregido un problema que impedía mostrar correctamente algunos textos de la interfaz.
* Corregido un problema con el sistema de idiomas que podía impedir que ciertas traducciones se mostraran correctamente

---

## [1.1.3] - 2026-05-30

### Added
* Se ha añadido verificación en dos pasos (2FA) para mayor seguridad al iniciar sesión
* Ahora puedes confirmar la desactivación del 2FA con un mensaje de seguridad
* Se solicita confirmación de contraseña antes de editar cualquier contraseña guardada
* Se ha añadido opción para mostrar u ocultar contraseñas en las ventanas de edición
* Ahora puedes cambiar el tamaño de la interfaz (pequeño, normal o grande)
* Se ha añadido modo claro además del modo oscuro
* Ahora puedes ajustar el contraste visual de la aplicación para mejorar la lectura
* La ventana de añadir nueva contraseña ahora es más simple y rápida de usar
* La ventana de edición muestra solo las opciones necesarias según los cambios realizados

### Changed
* Se ha mejorado la experiencia general de edición de contraseñas
* Se ha optimizado la visualización de elementos en toda la aplicación

### Fixed
* Se ha corregido un problema donde la ventana de añadir contraseña no se cerraba correctamente
* Se ha solucionado la duplicación del logo en la barra lateral
* El campo de código de verificación ahora se muestra correctamente centrado
* Se ha eliminado una línea visual innecesaria en la parte superior de la aplicación
* Se ha mejorado la carga del logotipo en toda la interfaz

---

## [1.1.2] - 2026-05-28

### Added
* Ahora puedes importar tus datos desde un archivo de copia de seguridad
* Ahora puedes exportar tus datos de forma segura en un archivo .slock
* Se ha añadido la opción de borrar todos los datos de la aplicación
* Se ha añadido bloqueo automático de la aplicación tras un periodo de inactividad
* Mensajes de confirmación al importar o exportar datos

### Fixed
* Se ha corregido un problema que impedía cargar correctamente los datos importados
* Se ha solucionado un error al exportar datos en algunos casos

---

## [1.1.1] - 2026-05-27

### Added
* Nueva pantalla de ajustes con todas las opciones principales:
  * Seguridad (2FA, cambio de contraseña, bloqueo automático)
  * Apariencia (tema claro/oscuro, contraste, tamaño de texto)
  * Gestión de datos (importar y exportar)
  * Zona de seguridad (borrado completo con confirmación)
* Ahora la aplicación puede cambiar entre tema claro y oscuro
* Se ha añadido soporte para ajustar el tamaño de la interfaz
* Se ha preparado el sistema para futuras mejoras visuales

### Changed
* Se ha mejorado la forma en que la aplicación guarda la información de forma más estable
* El inicio de sesión ahora utiliza la contraseña real del usuario
* La primera vez que se abre la aplicación, el usuario puede crear su contraseña maestra de forma guiada

### Fixed
* Se han corregido problemas de estabilidad al guardar información
* Se han solucionado errores en la navegación entre pantallas

---

## [1.0.2] - 2026-05-25

### Added
* Panel principal donde puedes ver todas tus contraseñas guardadas
* Ventana para añadir nuevas contraseñas de forma sencilla
* Ventana para editar y eliminar contraseñas existentes
* Generador de contraseñas con opciones de personalización
* Navegación entre secciones (inicio, generador y ajustes)
* Botones personalizados de ventana (minimizar, maximizar y cerrar)
* Diseño base completo de la aplicación
* Número de versión visible dentro de la aplicación

### Changed
* Se ha mejorado la navegación entre secciones para hacerla más fluida
* Se ha reforzado la estabilidad general de la aplicación

### Fixed
* Se ha corregido el sistema de inicio de sesión
* Se han solucionado problemas en la respuesta de botones y elementos de la interfaz

---

## [1.0.0] - 2026-05-23

### Added
* Pantalla de inicio de sesión
* Estructura inicial de la aplicación
* Diseño base de todas las pantallas principales
* Sistema de navegación entre secciones
* Logotipo y estilo visual inicial
