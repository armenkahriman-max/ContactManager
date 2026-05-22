import { loadContacts, saveContacts } from "./storage.js";
import { renderContacts } from "./render.js";
import {
    addContact,
    deleteContact,
    searchContact,
    editContact
} from "./contacts.js";

// DOM Selection
const addButton = document.querySelector(".add-btn");
const addInput = document.querySelector(".add-input");

const searchButton = document.querySelector(".search-btn");
const searchInput = document.querySelector(".search-input");

const showBtn = document.querySelector(".show-btn");
const clientList = document.querySelector(".client-list");

// Load contacts
let contacts = loadContacts();

// Refresh UI
function refreshContacts() {
    renderContacts(
        clientList,
        contacts,
        handleDelete,
        handleEdit
    );
}

// Delete handler
function handleDelete(name) {
    deleteContact(contacts, name);

    saveContacts(contacts);

    refreshContacts();
}

// Edit handler
function handleEdit(oldName) {
    const newName = prompt(
        "Enter new name:",
        oldName
    );

    if (!newName || newName.trim() === "") {
        return;
    }

    const success = editContact(
        contacts,
        oldName,
        newName.trim()
    );

    if (!success) {
        alert("Client not found");
        return;
    }

    saveContacts(contacts);

    refreshContacts();
}

// Initial render
refreshContacts();

// Add contact
addButton.addEventListener("click", function () {

    const name = addInput.value.trim();

    if (name === "") {
        alert("Client name cannot be empty");
        return;
    }

    addContact(contacts, name);

    saveContacts(contacts);

    refreshContacts();

    addInput.value = "";
});

// Search contact
searchButton.addEventListener("click", function () {

    const searchName = searchInput.value.trim();

    const found = searchContact(
        contacts,
        searchName
    );

    if (found) {
        alert(`Found: ${found}`);
    } else {
        alert("Client not found");
    }
});

// Show / Hide contacts
let showing = true;

showBtn.addEventListener("click", function () {

    showing = !showing;

    if (showing) {
        clientList.style.display = "block";
        showBtn.textContent = "Hide Clients";
    } else {
        clientList.style.display = "none";
        showBtn.textContent = "Show All Clients";
    }
});
