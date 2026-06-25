// Idiomas
const translations = {
    es: {
        title: 'SAILOCK',
        subtitle1: 'Un gestor de contraseñas local-first para Windows.',
        subtitle2: 'Sin nube, sin internet —',
        description: 'tus datos nunca abandonan tu dispositivo.',
        download: 'DESCARGAR SAILOCK',
        portable: 'Portátil',
        modalTitle: 'Idioma',
        footer: '© 2026 SAILOCK - Todos los derechos reservados',
        feat1: 'AES-256<br>CIFRADO',
        feat2: '2FA<br>TOTP',
        feat3: 'LOCAL<br>PRIMERO',
        feat4: 'BLOQUEO<br>AUTOMÁTICO',
        feat5: 'CÓDIGO<br>ABIERTO',
        feat6: 'SIN NUBE<br>SIN INTERNET'
    },
    en: {
        title: 'SAILOCK',
        subtitle1: 'A local-first password manager for Windows.',
        subtitle2: 'No cloud, no internet —',
        description: 'your data never leaves your device.',
        download: 'DOWNLOAD SAILOCK',
        portable: 'Portable',
        modalTitle: 'Language',
        footer: '© 2026 SAILOCK - All rights reserved',
        feat1: 'AES-256<br>ENCRYPTED',
        feat2: '2FA<br>TOTP',
        feat3: 'LOCAL<br>FIRST',
        feat4: 'AUTO<br>LOCK',
        feat5: 'OPEN<br>SOURCE',
        feat6: 'NO CLOUD<br>NO INTERNET'
    },
    fr: {
        title: 'SAILOCK',
        subtitle1: 'Un gestionnaire de mots de passe local-first pour Windows.',
        subtitle2: 'Pas de cloud, pas d\'internet —',
        description: 'vos données ne quittent jamais votre appareil.',
        download: 'TÉLÉCHARGER SAILOCK',
        portable: 'Portable',
        modalTitle: 'Langue',
        footer: '© 2026 SAILOCK - Tous droits réservés',
        feat1: 'AES-256<br>CHIFFRÉ',
        feat2: '2FA<br>TOTP',
        feat3: 'LOCAL<br>D\'ABORD',
        feat4: 'VERROUILLAGE<br>AUTO',
        feat5: 'SOURCE<br>OUVERTE',
        feat6: 'SANS CLOUD<br>SANS INTERNET'
    },
    de: {
        title: 'SAILOCK',
        subtitle1: 'Ein lokaler Passwort-Manager für Windows.',
        subtitle2: 'Keine Cloud, kein Internet —',
        description: 'Ihre Daten verlassen Ihr Gerät nie.',
        download: 'SAILOCK HERUNTERLADEN',
        portable: 'Portabel',
        modalTitle: 'Sprache',
        footer: '© 2026 SAILOCK - Alle Rechte vorbehalten',
        feat1: 'AES-256<br>VERSCHLÜSSELT',
        feat2: '2FA<br>TOTP',
        feat3: 'LOKAL<br>ZUERST',
        feat4: 'AUTO<br>SPERREN',
        feat5: 'OPEN<br>SOURCE',
        feat6: 'KEINE CLOUD<br>KEIN INTERNET'
    }
};

const langNames = {
    es: 'ES',
    en: 'EN',
    fr: 'FR',
    de: 'DE'
};

let currentLang = 'es';

// Cargar idioma guardado
document.addEventListener('DOMContentLoaded', () => {
    const saved = localStorage.getItem('sailockLang') || 'es';
    setLanguage(saved);
    
    // Lang button
    document.getElementById('langBtn').addEventListener('click', toggleLangModal);
    
    // Lang options
    document.querySelectorAll('.lang-option').forEach(btn => {
        btn.addEventListener('click', (e) => {
            setLanguage(e.target.dataset.lang);
            document.getElementById('langModal').classList.remove('active');
        });
    });
    
    // Download button
    document.getElementById('downloadBtn').addEventListener('click', () => {
        window.location.href = 'https://github.com/Sailok25/Sailock/releases';
    });
    
    // Cerrar modal al hacer click fuera
    document.addEventListener('click', (e) => {
        const modal = document.getElementById('langModal');
        const btn = document.getElementById('langBtn');
        if (!modal.contains(e.target) && !btn.contains(e.target)) {
            modal.classList.remove('active');
        }
    });
});

function setLanguage(lang) {
    currentLang = lang;
    localStorage.setItem('sailockLang', lang);
    
    // Actualizar botón (solo mostrar código del idioma)
    const langSpan = document.querySelector('.lang-btn span');
    if (langSpan) {
        langSpan.textContent = langNames[lang];
    }
    
    // Actualizar título del modal
    document.getElementById('modalTitle').textContent = translations[lang].modalTitle;
    
    // Actualizar todas las traducciones
    document.querySelectorAll('[data-i18n]').forEach(el => {
        const key = el.dataset.i18n;
        el.innerHTML = translations[lang][key] || translations.en[key];
    });
    
    // Actualizar lang options activo
    document.querySelectorAll('.lang-option').forEach(btn => {
        btn.classList.remove('active');
        if (btn.dataset.lang === lang) {
            btn.classList.add('active');
        }
    });
    
    // Cambiar atributo lang del HTML
    document.documentElement.lang = lang;
}

function toggleLangModal() {
    document.getElementById('langModal').classList.toggle('active');
}