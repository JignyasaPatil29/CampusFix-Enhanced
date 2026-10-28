(() => {
  const root = document.documentElement;
  const toggle = document.getElementById('themeToggle');
  const savedTheme = localStorage.getItem('campusfix-theme');
  if (savedTheme === 'dark') root.setAttribute('data-bs-theme', 'dark');
  const updateThemeIcon = () => {
    if (!toggle) return;
    const dark = root.getAttribute('data-bs-theme') === 'dark';
    toggle.innerHTML = dark ? '<i class="bi bi-sun"></i>' : '<i class="bi bi-moon-stars"></i>';
    toggle.setAttribute('aria-label', dark ? 'Switch to light mode' : 'Switch to dark mode');
  };
  updateThemeIcon();
  toggle?.addEventListener('click', () => {
    const next = root.getAttribute('data-bs-theme') === 'dark' ? 'light' : 'dark';
    root.setAttribute('data-bs-theme', next);
    localStorage.setItem('campusfix-theme', next);
    updateThemeIcon();
  });

  const counters = document.querySelectorAll('.count-up');
  const animateCounter = (el) => {
    const target = Number(el.dataset.count || 0);
    if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
      el.textContent = target.toLocaleString();
      return;
    }
    const duration = 850;
    const start = performance.now();
    const tick = (now) => {
      const progress = Math.min((now - start) / duration, 1);
      const eased = 1 - Math.pow(1 - progress, 4);
      el.textContent = Math.round(target * eased).toLocaleString();
      if (progress < 1) requestAnimationFrame(tick);
    };
    requestAnimationFrame(tick);
  };
  if ('IntersectionObserver' in window) {
    const observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          if (entry.target.classList.contains('count-up')) animateCounter(entry.target);
          observer.unobserve(entry.target);
        }
      });
    }, { threshold: 0.25 });
    counters.forEach(el => observer.observe(el));
  } else counters.forEach(animateCounter);

  document.querySelectorAll('.form-control, .form-select').forEach(el => {
    el.addEventListener('focus', () => el.closest('.mb-3, .col-md-6, .col-12')?.classList.add('field-focused'));
    el.addEventListener('blur', () => el.closest('.mb-3, .col-md-6, .col-12')?.classList.remove('field-focused'));
  });

  const successAlert = document.querySelector('.alert-success');
  if (successAlert) {
    successAlert.classList.add('reveal');
    window.setTimeout(() => {
      successAlert.style.transition = 'opacity .35s ease, transform .35s ease';
      successAlert.style.opacity = '0';
      successAlert.style.transform = 'translateY(-8px)';
      window.setTimeout(() => successAlert.remove(), 400);
    }, 5000);
  }
})();
