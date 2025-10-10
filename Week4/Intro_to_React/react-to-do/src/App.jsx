import { useState } from 'react';
import './App.css';

function TaskItem({ task, onToggle, onDelete }) {
  return (
    <div style={{ 
      padding: '10px', 
      margin: '10px 0', 
      border: '1px solid #ddd',
      borderRadius: '4px',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      backgroundColor: task.completed ? '#f0f0f0' : 'white'
    }}>
      <div style={{ display: 'flex', alignItems: 'center', flex: 1 }}>
        <input
          type="checkbox"
          checked={task.completed}
          onChange={() => onToggle(task.id)}
          style={{ marginRight: '10px' }}
        />
        <p style={{ 
          margin: 0,
          textDecoration: task.completed ? 'line-through' : 'none',
          color: task.completed ? '#888' : '#333'
        }}>
          {task.text}
        </p>
      </div>
      <button
        onClick={() => onDelete(task.id)}
        style={{
          padding: '5px 10px',
          backgroundColor: '#f44336',
          color: 'white',
          border: 'none',
          borderRadius: '4px',
          cursor: 'pointer'
        }}
      >
        Delete
      </button>
    </div>
  );
}

function App() {
  const [tasks, setTasks] = useState([
    { id: 1, text: 'Learn React', completed: false },
    { id: 2, text: 'Build a project', completed: false },
    { id: 3, text: 'Deploy to production', completed: false }
  ]);

  const [newTaskText, setNewTaskText] = useState('');

  const addTask = () => {
    if (newTaskText.trim() === '') return;

    const newTask = {
      id: Date.now(),
      text: newTaskText,
      completed: false
    };

    setTasks([...tasks, newTask]);
    setNewTaskText('');
  };

  // Toggle task completion
  const toggleTask = (id) => {
    setTasks(tasks.map(task =>
      task.id === id 
        ? { ...task, completed: !task.completed }
        : task
    ));
  };

  // Delete a task
  const deleteTask = (id) => {
    setTasks(tasks.filter(task => task.id !== id));
  };

  return (
    <div className="App">
      <h1>My Task List</h1>
      
      <div style={{ marginBottom: '20px' }}>
        <input
          type="text"
          value={newTaskText}
          onChange={(e) => setNewTaskText(e.target.value)}
          onKeyPress={(e) => e.key === 'Enter' && addTask()}
          placeholder="Enter a new task"
          style={{
            padding: '8px',
            width: '70%',
            fontSize: '16px'
          }}
        />
        <button 
          onClick={addTask}
          style={{
            padding: '8px 16px',
            marginLeft: '10px',
            fontSize: '16px',
            backgroundColor: '#4CAF50',
            color: 'white',
            border: 'none',
            borderRadius: '4px',
            cursor: 'pointer'
          }}
        >
          Add Task
        </button>
      </div>

      <div>
        {tasks.length === 0 ? (
          <p style={{ textAlign: 'center', color: '#888' }}>
            No tasks yet. Add one above!
          </p>
        ) : (
          tasks.map(task => (
            <TaskItem 
              key={task.id} 
              task={task}
              onToggle={toggleTask}
              onDelete={deleteTask}
            />
          ))
        )}
      </div>

      <div style={{ marginTop: '20px', color: '#666' }}>
        <p>
          Total: {tasks.length} | 
          Completed: {tasks.filter(t => t.completed).length} | 
          Remaining: {tasks.filter(t => !t.completed).length}
        </p>
      </div>
    </div>
  );
}

export default App;