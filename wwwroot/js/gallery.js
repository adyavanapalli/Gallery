/**
 * Gets the HTML string for a card with the specified parameters.
 * @param {number} id The ID of the image.
 * @param {string} name The name of the image.
 * @param {string} thumbnailPixelData The thumbnail pixel data of the image. This is in base64 encoding.
 * @returns The HTML string for a card with the specified parameters
 */
function getCardHtml(id, name, thumbnailPixelData) {
  return `
  <div class="col" data-id="${id}">
    <div class="card h-100 text-center">
      <img src="data:image;base64,${thumbnailPixelData}" height="100" width="100" class="picture" />
      <div class="card-body">
        <p class="card-title my-3 js-card-title">${name}</p>
        <div class="d-flex justify-content-between align-items-center">
            <div class="btn-group">
              <button type="button" class="btn btn-sm btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#viewModal" data-bs-view-id="${id}">View</button>
              <button type="button" class="btn btn-sm btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#editModal" data-bs-edit-id="${id}">Edit</button>
            </div>
            <div class="btn-group">
              <button type="button" class="btn btn-sm btn-outline-danger delete-button" onclick="deletePicture(${id})">Delete</button>
            </div>
          </div>
      </div>
    </div>
  </div>
`;
}

/**
 * Sets up the event listener for the `shown.bs.modal` event on the edit modal.
 */
function setUpEditModalShownListener() {
  document
    .getElementById("editModal")
    .addEventListener("shown.bs.modal", function (event) {
      var button = event.relatedTarget;
      var id = button.getAttribute("data-bs-edit-id");

      document
        .querySelector("[data-bs-save-id]")
        .setAttribute("data-bs-save-id", id);
    });
}

/**
 * Sets up the event listener for the `shown.bs.modal` event on the view modal.
 */
function setUpViewModalShownListener() {
  document
    .getElementById("viewModal")
    .addEventListener("show.bs.modal", async function (event) {
      var button = event.relatedTarget;
      var id = button.getAttribute("data-bs-view-id");

      var response = await fetch(`/api/v1/images/${id}`);
      var image = await response.json();

      document.querySelector(
        ".js-view-modal-body"
      ).innerHTML = `<img class="modal-picture" src="data:image;base64,${image.imagePixelData}" />`;
    });
}

/**
 * Loads all existing pictures' thumbnails in the gallery's data store.
 */
async function loadExistingPictureThumbnails() {
  var response = await fetch("/api/v1/images?thumbnailOnly=true");
  var images = await response.json();

  var $galleryContainer = document.querySelector(".js-gallery-container");

  for (const image of images) {
    var cardHtml = getCardHtml(image.id, image.name, image.thumbnailPixelData);

    $galleryContainer.innerHTML += cardHtml;
  }

  setUpEditModalShownListener();
  setUpViewModalShownListener();
}

/**
 * Uploads the picture specified in the file input.
 */
async function uploadPicture() {
  var $fileInput = document.querySelector(".js-file-input");

  if ($fileInput.files.length === 1) {
    var file = $fileInput.files[0];

    var formData = new FormData();
    formData.append("formFile", file);

    var response = await fetch("/api/v1/images", {
      method: "POST",
      body: formData,
    });
    var image = await response.json();

    var $galleryContainer = document.querySelector(".js-gallery-container");

    var cardHtml = getCardHtml(image.id, image.name, image.thumbnailPixelData);
    $galleryContainer.innerHTML += cardHtml;
  }

  setUpEditModalShownListener();
  setUpViewModalShownListener();
}

/**
 * Deletes the picture with the specified ID.
 * @param {number} id The ID of the picture to delete.
 */
async function deletePicture(id) {
  await fetch(`/api/v1/images/${id}`, {
    method: "DELETE",
  });

  document.querySelector(`[data-id='${id}']`).remove();
}

/**
 * Renames the picture with the context specific ID with the context specific
 * name.
 */
async function updatePictureName($el) {
  var id = $el.getAttribute("data-bs-save-id");
  var name = document.getElementById("edit-name-input").value;

  var patchModel = {
    name: name,
  };

  await fetch(`/api/v1/images/${id}`, {
    method: "PATCH",
    body: JSON.stringify(patchModel),
    headers: { "Content-Type": "application/json" },
  });

  document
    .querySelector(`[data-id='${id}']`)
    .querySelector(".js-card-title").textContent = name;
}
