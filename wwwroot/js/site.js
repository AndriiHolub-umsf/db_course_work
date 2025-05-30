// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getToken() {
    return localStorage.getItem("jwtToken");
}

function requireAuth() {
    if (!getToken()) {
        window.location = "/login.html";
    }
}

function logout() {
    localStorage.removeItem("jwtToken");
    window.location = "/index.html";
}

function isLoggedIn() {
    return !!getToken();
}

// (по желанию) получить payload JWT и, например, роль
function getJwtPayload() {
    const token = getToken();
    if (!token) return null;
    try {
        const payloadBase64 = token.split('.')[1];
        const json = atob(payloadBase64);
        return JSON.parse(json);
    } catch { return null; }
}

function toLocaleDate(date) {

    const outdate = new Date(date);
    return outdate.toLocaleDateString('ru-RU')
}
const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";


fetch('/header.html')
    .then(res => res.text())
    .then(html => {
        document.getElementById('header').innerHTML = html;

        // Логика для меню в зависимости от логина/роли
        if (isLoggedIn()) {
            document.getElementById('nav-auth').style.display = 'none';
            document.getElementById('logoutLink').style.display = 'inline';
            document.getElementById('logoutLink').onclick = logout;

            // Показываем admin панель, если роль — Admin
            const payload = getJwtPayload();
            if (payload) {

                if (payload.role === 'Admin' || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin') {
                    document.getElementById('adminPanelLink').style.display = 'inline';
                }
                else {
                    document.getElementById('playerPanelLink').style.display = 'inline';
                }
            }
        } else {
            document.getElementById('nav-auth').style.display = 'inline';
            document.getElementById('logoutLink').style.display = 'none';
            document.getElementById('adminPanelLink').style.display = 'none';
            document.getElementById('playerPanelLink').style.display = 'none';
        }
    });