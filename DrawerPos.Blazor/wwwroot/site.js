function initializeSidebarToggle() {
    const sidebar = document.getElementById('sidebar');
    const toggleButton = document.querySelector('button[onclick^="ToggleNavMenu"]');

    if (sidebar && toggleButton) {
        toggleButton.addEventListener('click', () => {
            sidebar.classList.toggle('w-64');
            sidebar.classList.toggle('w-16');
        });
    }
}
