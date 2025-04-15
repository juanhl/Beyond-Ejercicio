const BASE_URL = "todolist";
export async function getItems() {
    const res = await fetch(`${BASE_URL}/items`);
    if (!res.ok) throw new Error("Error fetching items");
    return res.json();
}

export async function getCategories() {
    const res = await fetch(`${BASE_URL}/categories`);
    if (!res.ok) throw new Error("Error fetching categories");
    return res.json();
}


export async function addItem(title, description, category) {
    const res = await fetch(`${BASE_URL}/items`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ title, description, category })
    });
    if (!res.ok) {
        const error = await res.text();
        throw new Error(error || "Error adding item");
    }
}

export async function updateItem(id, description) {
    const res = await fetch(`${BASE_URL}/items/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ description })
    });
    if (!res.ok) {
        const error = await res.text();
        throw new Error(error || "Error updating item");
    }
}

export async function deleteItem(id) {
    const res = await fetch(`${BASE_URL}/items/${id}`, {
        method: "DELETE"
    });
    if (!res.ok) {
        const error = await res.text();
        throw new Error(error || "Error deleting item");
    }
}

export async function addProgress(id, percent) {
    const res = await fetch(`${BASE_URL}/items/${id}/progress`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ percent })
    });
    if (!res.ok) {
        const error = await res.text();
        throw new Error(error || "Error adding progress");
    }
}