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


const resultBox = document.getElementById("search-result");
const clientNameText= document.getElementById("client-name");

const editFoundBtn = document.getElementById("edit-found-btn");

const deleteFoundBtn = document.getElementById("delete-found-btn");

let foundClient =null;




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
        foundClient = found;

        clientNameText.textContent =
        `Name: ${found}`;

        resultBox.classList.remove("hidden");
    } else {
        
        resultBox.classList.add("hidden");

        alert("Client not foud");
    }
});

editFoundBtn.addEventListener("click",  function() {
    if (!foundClient) return;

    handleEdit(foundClient);

    foundClient = null;
    resultBox.classList.add("hidden");
});

deleteFoundBtn.addEventListener("click", function() {

    if (!foundClient) return;

    handleDelete(foundClient);
    foundClient = null;

    resultBox.classList.add("hidden");
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
