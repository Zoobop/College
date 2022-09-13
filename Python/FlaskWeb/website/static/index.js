function deleteItem(itemId) 
{
    fetch("/delete-note", {
      method: "POST",
      body: JSON.stringify({ itemId: itemId }),
    }).then((_res) => {
      window.location.href = "/";
    });
}