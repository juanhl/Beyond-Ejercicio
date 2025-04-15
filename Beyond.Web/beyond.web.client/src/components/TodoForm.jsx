import React, { useState } from 'react';

const TodoForm = ({ categories, onItemAdded }) => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [category, setCategory] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        await fetch('/todolist/items', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title, description, category })
        });
        setTitle('');
        setDescription('');
        setCategory('');
        onItemAdded();
    };

    return (
        <form className="todo-item-form" onSubmit={handleSubmit}>
            <input value={title} onChange={e => setTitle(e.target.value)} placeholder="Title" required />
            <input value={description} onChange={e => setDescription(e.target.value)} placeholder="Description" />
            <select
                value={category}
                onChange={e => setCategory(e.target.value)}
                required
            >
                <option value="" disabled>Select a category</option>
                {categories.map((c, i) => (
                    <option key={i} value={c}>
                        {c}
                    </option>
                ))}
            </select>
            <button type="submit">Add Item</button>
        </form>
    );
};

export default TodoForm;