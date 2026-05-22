export function renderContacts(
    clientList,
    contacts,
    deleteHandler,
    editHandler
) {
    clientList.innerHTML = "";

    contacts.forEach(contact => {

        const clientItem =
            document.createElement("div");

        clientItem.classList.add("client-item");

        clientItem.innerHTML = `
            <span>${contact}</span>

            <div class="actions">
                <button class="edit-btn">
                    Edit
                </button>

                <button class="delete-btn">
                    Delete
                </button>
            </div>
        `;

        const editButton =
            clientItem.querySelector(".edit-btn");

        editButton.addEventListener(
            "click",
            function () {
                editHandler(contact);
            }
        );

        const deleteButton =
            clientItem.querySelector(".delete-btn");

        deleteButton.addEventListener(
            "click",
            function () {
                deleteHandler(contact);
            }
        );

        clientList.appendChild(clientItem);
    });
}