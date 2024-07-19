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
window.applyFilter = (filterValue) => {
    let rows = document.querySelectorAll("#productsTable tbody tr");
    filterValue = filterValue.trim().toLowerCase();

    rows.forEach(row => {
        let productName = row.querySelector(".product-name").textContent.trim().toLowerCase();
        let displayName = row.querySelector(".display-name").textContent.trim().toLowerCase();
        let category = row.querySelector(".category").textContent.trim().toLowerCase();
        let price = row.querySelector(".price").textContent.trim().toLowerCase();
        let quantity = row.querySelector(".quantity").textContent.trim().toLowerCase();
        let unit = row.querySelector(".unit").textContent.trim().toLowerCase();
        let status = row.querySelector(".status").textContent.trim().toLowerCase();
        let description = row.querySelector(".description").textContent.trim().toLowerCase();

        if (
            productName.includes(filterValue) ||
            displayName.includes(filterValue) ||
            category.includes(filterValue) ||
            price.includes(filterValue) ||
            quantity.includes(filterValue) ||
            unit.includes(filterValue) ||
            status.includes(filterValue) ||
            description.includes(filterValue)
        ) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
   
}
window.toggleSidebar = function () {
    const sidebar = document.querySelector('.sidebar');
    const isOpen = sidebar.style.width === '16rem';
    sidebar.style.width = isOpen ? '5rem' : '16rem';
    return !isOpen;
};
function toggleDrawer() {
    var drawer = document.getElementById('drawer-right');
    var backdrop = document.getElementById('drawer-backdrop');

    if (drawer.classList.contains('hidden')) {
        drawer.classList.remove('hidden');
        backdrop.classList.remove('hidden');
    } else {
        drawer.classList.add('hidden');
        backdrop.classList.add('hidden');
    }
}

document.addEventListener("DOMContentLoaded", function () {
    var dropdownButton = document.getElementById("dropdownActionButton");
    if (dropdownButton) {
        dropdownButton.addEventListener("click", function () {
            var dropdown = document.getElementById("dropdownAction");
            if (dropdown) {
                dropdown.classList.toggle("hidden");
            }
        });
    }
});