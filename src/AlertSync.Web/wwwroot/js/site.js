(() => {
    const shell = document.querySelector(".app-shell");
    document.querySelector("[data-sidebar-toggle]")?.addEventListener("click", () => {
        shell?.classList.toggle("sidebar-open");
    });

    const modalEl = document.getElementById("confirmModal");
    if (!modalEl || !window.bootstrap) {
        return;
    }

    const modal = new bootstrap.Modal(modalEl);
    const form = modalEl.querySelector("form");
    const text = modalEl.querySelector("[data-confirm-text]");

    document.querySelectorAll("[data-confirm]").forEach((button) => {
        button.addEventListener("click", () => {
            if (!form || !text) {
                return;
            }

            text.textContent = button.getAttribute("data-confirm") || "Continue?";
            form.action = button.getAttribute("data-action") || "#";
            modal.show();
        });
    });
})();
