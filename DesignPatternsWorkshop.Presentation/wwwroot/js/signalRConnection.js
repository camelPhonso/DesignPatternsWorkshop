import addProduct from "/js/addProduct.js"
import removeProduct from "/js/removeProduct.js"
import undoLast from "/js/undoLast.js"
import redoLast from "/js/redoLast.js"
import addDiscount from "/js/addDiscount.js"

export const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5130/purchase-hub")
  .withAutomaticReconnect()
  .build()

connection.start().catch(error => console.error(error))

connection.on("UpdatePurchase", function () {
  fetch("Home/GetProductList")
    .then(response => response.text())
    .then(
      html => (document.getElementById("purchaseContainer").innerHTML = html)
    )
    .catch(error => console.error(error))

  fetch("Home/GetTotal")
    .then(response => response.text())
    .then(
      html => (document.getElementById("purchaseTotalDisplay").innerHTML = html)
    )
    .catch(error => console.error(error))
})

document.addEventListener("keydown", event => {
    if (!event.ctrlKey) return;

    if (event.key === "ArrowLeft") return connection.invoke("Undo");
    if (event.key === "ArrowRight") return connection.invoke("Redo");
    if (event.key === "ArrowUp") return connection.invoke("AddProduct", { Name: 'Oat milk', Price: 2, Quantity: 1 });
});

window.addProduct = addProduct
window.removeProduct = removeProduct
window.undoLast = undoLast
window.redoLast = redoLast
window.addDiscount = addDiscount
