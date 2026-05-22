export function loadContacts() {
    const json = localStorage.getItem("contacts");

    return json === null
    ? []
    : JSON.parse(json);
}

export function saveContacts(contacts) {
localStorage.setItem(
    "contacts",
    JSON.stringify(contacts)
);
}