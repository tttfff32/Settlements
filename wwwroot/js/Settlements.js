const uri = "/Settlement";
let settlements = [];

const pageSize = 5;
var currentPage = 1;

function loadPage(pageNumber) {
  fetch(`${uri}/page/${pageNumber}?pageSize=${pageSize}`)
    .then((response) => response.json())
    .then((data) => {
      if (data.settlements.length > 0) {
        settlements = data.settlements;
        _displayItems(settlements);
        currentPage = data.currentPage;
      }
    });
}

function PreviousPage() {
  if (currentPage > 1) {
    loadPage(currentPage - 1);
  } else loadPage(currentPage);
}

function NextPage() {
  loadPage(currentPage + 1);
}


async function addItem() {
  const addNameTextbox = document.getElementById("add-name");
  const item = {
    name: addNameTextbox.value.trim(),
  };

  await fetch(uri, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  })
    .then((response) => response.json())
    .then(() => {
      loadPage(currentPage);
      addNameTextbox.value = "";
    })
    .catch((error) => console.error("Unable to add item.", error));
}

async function deleteItem(id) {
  await fetch(`${uri}/${id}`, {
    method: "DELETE",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  })
    .then(() => loadPage(currentPage))
    .catch((error) => console.error("Unable to delete item.", error));
}

function displayEditForm(id) {
  const item = settlements.find((item) => item.id === id);

  document.getElementById("edit-name").value = item.name;
  document.getElementById("edit-id").value = item.id;
  document.getElementById("editForm").style.display = "block";
}

async function updateItem() {
  const itemId = document.getElementById("edit-id").value;
  const item = {
    id: parseInt(itemId, 10),
    name: document.getElementById("edit-name").value.trim(),
  };

  await fetch(`${uri}/${itemId}`, {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  })
    .then(() => loadPage(currentPage))
    .catch((error) => console.error("Unable to update item.", error));

  closeInput();

  return false;
}

async function searchSettlements() {
  const searchTextbox = document.getElementById("search-box");
  const searchTerm = searchTextbox.value.trim();
  const searchUri =
    searchTerm === ""
      ? `${uri}/page/${currentPage}?pageSize=${pageSize}`
      : `${uri}/filter/${encodeURIComponent(searchTerm)}`;

  await fetch(searchUri, {
    headers: {
      Accept: "application/json",
    },
  })
    .then((response) => response.json())
    .then((data) => {
      if (searchTerm === "") {
        settlements = data.settlements;
        _displayItems(settlements);
      } else {
        _displayItems(data);
      }
    })
    .catch((error) => console.error("Unable to get items.", error));
}
let sortOrder = "asc";

function sortItems(order) {
  sortOrder = order;
  _displayItems(sortData(settlements, sortOrder));
}

function sortData(data, order) {
  return data.sort((a, b) => {
    const comparison = a.name.localeCompare(b.name);
    return order === "asc" ? comparison : -comparison;
  });
}

function closeInput() {
  document.getElementById("editForm").style.display = "none";
}

function _displayItems(data) {
  const tBody = document.getElementById("Settlements");
  tBody.innerHTML = "";

  data.forEach((settlement) => {
    const button = document.createElement("button");

    let editButton = button.cloneNode(false);
    editButton.innerText = "Edit";
    editButton.setAttribute("onclick", `displayEditForm(${settlement.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = "Delete";
    deleteButton.setAttribute("onclick", `deleteItem(${settlement.id})`);

    let tr = tBody.insertRow();

    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(settlement.name);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    td2.appendChild(editButton);

    let td3 = tr.insertCell(2);
    td3.appendChild(deleteButton);
  });
  settlements = data;
}
loadPage(currentPage);
