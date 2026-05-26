export function addContact(contacts, name) {

    contacts.push(name);
}

export function deleteContact(contacts, name) {

    const index = contacts.indexOf(name);

    if (index !== -1) {
        contacts.splice(index, 1);
    }
}
export function searchContact(contacts, searchName) {

    return contacts.find(
        contact =>
            contact.toLowerCase() ===
            searchName.toLowerCase()
    );
}
export function editContact(
    contacts,
    oldName,
    newName
) {

    const index = contacts.findIndex(
        contact=>
            contact.toLowerCase() ===
        oldName.toLowerCase()

    );

    if (index !== -1) {

        contacts[index] = newName;

        return true;
    }
    return false;
}