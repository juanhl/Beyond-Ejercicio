import React, { useState } from 'react';
import { deleteItem, addProgress, updateItem } from '@/api/todoListApi';
import ProgressBar from '@/components/ProgressBar';

const TodoItem = ({ item, onItemRemoved, onItemUpdated }) => {
    const [progress, setProgress] = useState('');
    const [description, setDescription] = useState(item.description);
    const [error, setError] = useState('');
    const totalPercent = item.progressions.reduce((acc, p) => acc + p.percent, 0);

    const handleClickDeleteButton = async (e) => {
        e.preventDefault();
        try {
            await deleteItem(item.id);
            onItemRemoved();
            setError(''); 
        } catch (err) {
            setError(err.message); 
        }
    };

    const handleSubmitForm = async (e) => {
        e.preventDefault();
        try {
            await addProgress(item.id, progress);
            onItemUpdated();
            setError('');
        } catch (err) {
            setError(err.message);
        }
    };

    const handleBlurDescription = async () => {
        try {
            await updateItem(item.id, description);
            onItemUpdated();
            setError('');
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div className={`todo-item ${item.isCompleted ? 'completed' : ''}`} >
            <h2>{item.id}. {item.title} <small>({item.category})</small></h2>
            <textarea disabled={item.isCompleted} rows="5" onChange={e => setDescription(e.target.value)} onBlur={handleBlurDescription} value={description}></textarea>
            {!item.isCompleted &&
                <>
                    <form className="todo-item-actions" onSubmit={handleSubmitForm}>
                        <input value={progress} onChange={e => setProgress(e.target.value)} placeholder="Progression" type="number" required />
                        <button type="submit">Add Progress</button>
                        <button type="button" onClick={handleClickDeleteButton}>Delete Item</button>
                    </form>
                    <ProgressBar percent={totalPercent} />
                </>
            }
            {error && <div className="error-message">{error}</div>}
        </div>
    );
};

export default TodoItem;