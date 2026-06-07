// ============================================
//  SAILOCK — MAIN APP SCRIPT
// ============================================

(function () {
  'use strict';

  // ─── State ────────────────────────────────
  let currentLang = localStorage.getItem('sailock_lang') || 'en';
  let currentSection = 'download';

  // ─── DOM refs ─────────────────────────────
  const navLinks     = document.querySelectorAll('.nav-link');
  const sections     = document.querySelectorAll('.section');
  const langBtns     = document.querySelectorAll('.lang-btn');
  const sidebar      = document.querySelector('.sidebar');

  // ─── Mobile toggle ────────────────────────
  // Inject toggle button
  const toggleBtn = document.createElement('button');
  toggleBtn.className = 'mobile-toggle';
  toggleBtn.textContent = '[ MENU ]';
  document.body.appendChild(toggleBtn);

  toggleBtn.addEventListener('click', () => {
    sidebar.classList.toggle('open');
  });

  // Close sidebar when clicking outside on mobile
  document.addEventListener('click', (e) => {
    if (
      sidebar.classList.contains('open') &&
      !sidebar.contains(e.target) &&
      e.target !== toggleBtn
    ) {
      sidebar.classList.remove('open');
    }
  });

  // ─── Navigation ───────────────────────────
  function showSection(sectionId) {
    currentSection = sectionId;

    sections.forEach(s => s.classList.remove('active'));
    navLinks.forEach(l => l.classList.remove('active'));

    const targetSection = document.getElementById('section-' + sectionId);
    if (targetSection) targetSection.classList.add('active');

    navLinks.forEach(l => {
      if (l.dataset.section === sectionId) l.classList.add('active');
    });

    // Close mobile menu after navigation
    sidebar.classList.remove('open');
  }

  navLinks.forEach(link => {
    link.addEventListener('click', (e) => {
      e.preventDefault();
      showSection(link.dataset.section);
    });
  });

  // ─── i18n ─────────────────────────────────
  function applyLanguage(lang) {
    if (!translations[lang]) return;
    currentLang = lang;
    localStorage.setItem('sailock_lang', lang);

    const t = translations[lang];

    // Update html lang attribute
    document.documentElement.lang = t.html_lang || lang;

    // Update all keyed elements
    document.querySelectorAll('[data-key]').forEach(el => {
      const key = el.dataset.key;
      if (t[key] !== undefined) {
        el.textContent = t[key];
      }
    });

    // Update active language button
    langBtns.forEach(btn => {
      btn.classList.toggle('active', btn.dataset.lang === lang);
    });
  }

  langBtns.forEach(btn => {
    btn.addEventListener('click', () => applyLanguage(btn.dataset.lang));
  });

  // ─── Init ─────────────────────────────────
  applyLanguage(currentLang);
  showSection(currentSection);

})();
