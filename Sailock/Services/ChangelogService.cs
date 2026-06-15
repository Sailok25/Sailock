using System.Collections.Generic;
using Sailock.Models;

namespace Sailock.Services
{
    public static class ChangelogService
    {
        public static List<ChangelogEntry> GetEntries() => new List<ChangelogEntry>
        {
            new ChangelogEntry
            {
                Version  = "1.3.1",
                Date     = "2026-06-15",
                IsLatest = true,
                Added = new List<string>
                {
                    "Tiempo de bloqueo automático configurable: 30 segundos, 1 minuto, 2 minutos o 5 minutos, o desactivado por completo"
                },
                Changed = new List<string>
                {
                    "El selector de Auto-Lock en Ajustes ahora es un desplegable, igual que el de idioma o tamaño de texto",
                    "La descripción de Auto-Lock en Ajustes ahora refleja el periodo de inactividad configurado"
                }
            },
            new ChangelogEntry
            {
                Version  = "1.3.0",
                Date     = "2026-06-08",
                IsLatest = false,
                Added = new List<string>
                {
                    "Implementado un tema claro completo en toda la aplicación",
                    "Añadida la posibilidad de cambiar la contraseña maestra con validaciones de seguridad",
                    "Unificadas las barras de desplazamiento en toda la aplicación con un estilo visual consistente",
                    "Mejorado el efecto hover en los botones de ventana (minimizar, maximizar y cerrar)"
                },
                Changed = new List<string>
                {
                    "Las barras de desplazamiento ahora utilizan el mismo diseño visual en todas las ventanas",
                    "La ventana Settings ahora utiliza ScrollViewer para mejorar la navegación en pantallas pequeñas",
                    "Mejorada la visibilidad del hover en la barra lateral cuando se utiliza el tema claro",
                    "Los indicadores Latest y Legacy del Changelog ahora utilizan colores personalizables",
                    "El ScrollViewer del Dashboard ahora se encuentra fuera de la tabla para una experiencia de navegación más fluida"
                },
                Fixed = new List<string>
                {
                    "Corregidos los textos que permanecían en color blanco en el tema claro",
                    "Corregida la visibilidad de checkboxes y toggles en el tema claro",
                    "Mejorada la visibilidad del hover en los botones de ventana en ambos temas",
                    "Corregido un problema que impedía mostrar la barra de desplazamiento en Generator cuando la ventana era reducida",
                    "Corregido el contraste visual de los indicadores del Changelog"
                }
            },
            new ChangelogEntry
            {
                Version  = "1.2.2",
                Date     = "2026-06-05",
                IsLatest = false,
                Fixed = new List<string>
                {
                    "Restaurada la visibilidad del botón de versión en la barra lateral",
                    "Corregido un problema que hacía el acceso al historial de versiones poco visible"
                }
            },
            new ChangelogEntry
            {
                Version  = "1.2.1",
                Date     = "2026-06-04",
                IsLatest = false,
                Fixed = new List<string>
                {
                    "Corregida la desincronización entre el campo de contraseña visible y oculto en los formularios",
                    "Solucionado el problema de caché visual en el campo de contraseña maestra al abrir edición repetidamente",
                    "Corregida la alineación inconsistente del campo de contraseña al alternar entre modo visible y oculto",
                    "Corregidos errores ortográficos en textos de la interfaz en español",
                },
                Changed = new List<string>
                {
                    "Fuente actualizada a JetBrains Mono para mejorar la distinción entre mayúsculas, minúsculas y caracteres similares",
                    "Ajustados los tamaños de interfaz: Small, Default y Large con mayor diferencia perceptible",
                    "El escalado de interfaz ahora no afecta a los modales ni formularios",
                    "La barra lateral escala junto al contenido al cambiar el tamaño de interfaz"
                }
            },
            new ChangelogEntry
            {
                Version  = "1.2.0",
                Date     = "2026-06-01",
                IsLatest = false,
                Added = new List<string>
                {
                    "Añadido soporte para los idiomas español e inglés",
                    "La selección de idioma ya está disponible en Ajustes y se guarda automáticamente",
                    "Todas las vistas, diálogos y mensajes del sistema han sido traducidos"
                },
                Changed = new List<string>
                {
                    "Mejorada la apariencia de las barras de desplazamiento en el Historial de cambios",
                    "Actualizado el estilo del botón de versión para una mayor coherencia visual"
                },
                Fixed = new List<string>
                {
                    "Corregido un problema que podía impedir que algunos elementos de la interfaz se cargaran correctamente",
                    "Mejorado el sistema de traducciones para garantizar la carga correcta de todos los idiomas"
                }
            },
            new ChangelogEntry
            {
                Version = "1.1.3",
                Date    = "2026-05-30",
                IsLatest = false,
                Added   = new List<string>
                {
                    "Auto-Lock de sesión funcional: cierre automático tras 5 minutos de inactividad",
                    "Ventana de historial de versiones accesible desde el número de versión",
                    "Sistema de versiones con Semantic Versioning (Major.Minor.Patch)",
                },
                Fixed   = new List<string>
                {
                    "Modal Add New ya no permanece abierto al navegar entre vistas",
                    "El tema claro/oscuro ahora persiste correctamente al reiniciar la aplicación",
                    "El tamaño de interfaz persiste y tiene diferencia visible entre las opciones",
                    "El contraste visual persiste correctamente al reiniciar",
                    "Logo duplicado en la barra lateral eliminado",
                    "Placeholder del campo TOTP ahora aparece centrado",
                }
            },
            new ChangelogEntry
            {
                Version = "1.1.2",
                Date    = "2026-05-28",
                Added   = new List<string>
                {
                    "Import de archivo .slock externo con validación de contraseña maestra",
                    "Export de datos cifrados a archivo .slock como copia de seguridad",
                    "Delete All Data con confirmación y reseteo de fábrica",
                    "Mensaje de estado en Settings tras operaciones de Import/Export",
                },
                Fixed   = new List<string>
                {
                    "Import ahora recarga el Dashboard correctamente tras importar",
                    "Export genera un archivo .slock válido en todos los casos",
                }
            },
            new ChangelogEntry
            {
                Version = "1.1.1",
                Date    = "2026-05-27",
                Added   = new List<string>
                {
                    "Pantalla Settings completa: Security, Accessibility, Data Management, Danger Zone",
                    "2FA con TOTP compatible con Google Authenticator, Authy y cualquier app TOTP",
                    "Modal de confirmación al desactivar 2FA",
                    "Verificación de contraseña maestra antes de editar una entrada",
                    "Campo de contraseña con toggle mostrar/ocultar (icono de ojo)",
                    "Contraste visual configurable: bordes marcados o sutiles",
                    "Tamaño de interfaz configurable: Small, Default, Large",
                    "Modo claro con cambio de logo automático",
                },
                Fixed   = new List<string>
                {
                    "Almacenamiento: entradas cifradas con AES-256 en archivo .slock",
                    "Login conectado a contraseña maestra real",
                    "Primera ejecución guiada para crear la contraseña maestra",
                }
            },
            new ChangelogEntry
            {
                Version = "1.0.2",
                Date    = "2026-05-25",
                Added   = new List<string>
                {
                    "Dashboard funcional: lista de contraseñas, búsqueda y filtrado en tiempo real",
                    "Modal Add New con campos: título, categoría, email, usuario, contraseña, nota",
                    "Modal Edit con campos prellenados y opción de borrar entrada",
                    "Generador de contraseñas: longitud, charset, exclusiones y prefijo",
                    "Sidebar con navegación entre Dashboard, Generator y Settings",
                    "Botones personalizados de ventana: minimizar, maximizar, cerrar",
                    "Icono de aplicación (.ico) en ejecutable y barra de tareas",
                },
                Fixed   = new List<string>
                {
                    "Estilos globales unificados en App.xaml",
                    "RelayCommand corregido con CommandManager.RequerySuggested",
                }
            },
            new ChangelogEntry
            {
                Version = "1.0.1",
                Date    = "2026-05-25",
                Added   = new List<string>
                {
                    "Dashboard con creación y borrado de entradas funcional",
                    "Generador de contraseñas funcional con criterios configurables",
                },
                Fixed   = new List<string>
                {
                    "Login: validación de contraseña maestra real en lugar de valor fijo",
                    "Correcciones de navegación entre vistas",
                }
            },
            new ChangelogEntry
            {
                Version = "1.0.0",
                Date    = "2026-05-23",
                Added   = new List<string>
                {
                    "Estructura inicial del proyecto con arquitectura MVVM",
                    "Pantalla de inicio de sesión con contraseña provisional",
                    "Navegación entre vistas con ContentControl y DataTemplates",
                    "Fuente personalizada Supermolot Light integrada",
                    "Logo Sailock en login y barra lateral",
                }
            },
        };
    }
}