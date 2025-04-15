import { useEffect, useState } from 'react';
import { getItems, getCategories } from '@/api/todoListApi';
import TodoItem from '@/components/TodoItem';
import TodoForm from '@/components/TodoForm';
import '@/App.css';

function App() {
    const [items, setItems] = useState([]);
    const [categories, setCategories] = useState([]);

    const fetchItems = async () => {
        const items = await getItems();
        setItems(items);
    };

    const fetchCategories = async () => {
        const categories = await getCategories();
        setCategories(categories);
    };

    useEffect(() => {
        fetchItems();
        fetchCategories();
    }, []);

    return (
        <div className="container">
            <h1>Todo List</h1>
            <TodoForm categories={categories} onItemAdded={fetchItems} />
            <div className="todo-items">
                {items.map(item => (
                    <TodoItem key={item.id} item={item} onItemRemoved={fetchItems} onItemUpdated={fetchItems} />
                ))}
            </div>
        </div>
    );
}

export default App;