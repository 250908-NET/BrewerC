# What is React?

React is a popular, open-source **JavaScript library** for building user interfaces (UIs), particularly for **single-page applications (SPAs)**. It was created by Facebook and is now maintained by a community of developers and companies.

The core idea of React is to allow developers to build complex UIs by breaking them down into small, isolated, and reusable pieces of code called **components**. Think of components like Lego bricks; you build individual bricks first and then assemble them to create a complete structure.

---------

## React vs. Vanilla JS: Key Differences

While React is built with JavaScript, it introduces several powerful concepts that fundamentally change how you approach building web applications compared to using "vanilla" HTML, CSS, and JavaScript.

### The DOM: Virtual vs. Real

* **Vanilla JS:** You directly manipulate the **Document Object Model (DOM)**. The DOM is a tree-like structure of your HTML. Directly changing it can be slow and inefficient, especially for complex applications, because the browser has to recalculate and repaint the screen for every little change.
* **React:** React uses a **Virtual DOM**. This is a lightweight copy of the real DOM kept in memory. When you make a change (e.g., a user clicks a button), React first updates this virtual copy. Then, it uses a "diffing" algorithm to figure out the most efficient way to update the *real* DOM to match the virtual one. This process, called **reconciliation**, minimizes direct manipulation of the actual DOM, resulting in much better performance.

### Programming Style: Declarative vs. Imperative

* **Vanilla JS (Imperative):** You write code that describes *how* to achieve a result step-by-step. For example: "find this `<div>` element, create a new `<p>` element, set its text content, and then append it to the `<div>`."
* **React (Declarative):** You describe *what* the UI should look like for a given state. You declare that when the data (state) is `X`, the component should render `Y`. React takes care of the "how" behind the scenes. This makes your code more predictable and easier to debug.

### Structure: Component-Based Architecture

* **Vanilla JS:** You might organize your code into files and functions, but there's no enforced structure. It's easy for HTML, CSS, and JS to become a tangled mess.
* **React:** Everything is a **component**. A component is a self-contained module that manages its own state and renders a piece of the UI. For example, a search bar, a button, and a user profile card could all be separate components. You then compose these components together to build your entire application, which promotes reusability and organization.

### Syntax: JSX (JavaScript XML)

React introduces **JSX**, a syntax extension for JavaScript. It lets you write HTML-like markup directly inside your JavaScript files.

**Vanilla JS (Separate files):**

```html
<!-- index.html -->
<div id="root"></div>
```

```javascript
// app.js
const root = document.getElementById('root');
const element = document.createElement('h1');
element.textContent = 'Hello, world!';
root.appendChild(element);
```

**React (JSX in one file):**

```jsx
// App.jsx
function App() {
  return <h1>Hello, world!</h1>;
}
```

This co-location of rendering logic and UI markup simplifies development and makes components easier to visualize.

---------

## Building Single-Page Applications (SPAs)

A **Single-Page Application (SPA)** is a web app that interacts with the user by dynamically rewriting the current web page with new data from the web server, instead of the default method of a web browser loading entire new pages. The goal is a faster, more fluid user experience, similar to a desktop application.

React is perfect for building SPAs because of its component-based architecture and efficient use of the Virtual DOM. When you navigate through a React-powered SPA, you aren't actually going to a new HTML page. Instead, React is just swapping out components on the fly and re-rendering only what's necessary, without a full page refresh.

---------

## Creating Your First React App

Let's create a new React application from the command line. We'll use **Vite**, a modern and extremely fast build tool for front-end development.

### Prerequisites

Before you start, make sure you have **Node.js** installed on your machine. Installing Node.js will also install **npm** (Node Package Manager), which we need to manage our project's dependencies.

You can download Node.js from [nodejs.org](https://nodejs.org/). Look for the installer for your system - you do NOT need to do this through terminal, there are executables (they're just a little hard to find at first glance!)

### Steps to Create the App

1. **Open your terminal** And navigate to your HTML/JS challenge Repo, and create a new branch, the `react-demo` branch. We'll add our practice project there, following similar git workflow of merging back to the `main` branch after each step, but making sure to commit any new work to the feature branch: `react-demo`.
    ```bas
    git checkout -b react-demo
    ```

2. **Run the create command.** This single command scaffolds a complete React project for you. Replace `my-react-app` with your desired project name.

    ```bash
    npm create vite@latest react_to_do
    ```

    The terminal will prompt you to select a framework (choose React) and a variant (choose JavaScript), and other setup configuration options. Recent versions will even let you start the app from the template, but for now say 'No'.

3. **Navigate into your new project directory.**

    ```bash
    cd react_to_do
    ```

4. **Install the dependencies.** This command reads the `package.json` file in your new project and downloads all the necessary libraries (like React itself).

    ```bash
    npm install
    ```

5. **Start the development server.**

    ```bash
    npm run dev
    ```

That's it! Your terminal will now show you a local URL (usually `http://localhost:5173`). Open that URL in your web browser, and you will see your brand new React application running. You can now open the project folder in your code editor and start building!

---------

## React Introduction Walkthrough

### Interactive Task List Application

This walkthrough guides you through building a simple task list application to introduce core React concepts. We'll be using the new react_to_do application.

---------

## Step 1: Your First Component (Static)

Let's start by getting a structure on the page, then we can develop from there.  

**Learning Points:**

* React components are just JavaScript functions that return JSX
* JSX looks like HTML but is actually JavaScript
* Components must return a single parent element
* Component names must start with a capital letter

**Replace `src/App.js` with:**

```jsx
function App() {
  return (
    <div className="App">
      <h1>My Task List</h1>
      <div>
        <p>Learn React</p>
        <p>Build a project</p>
        <p>Deploy to production</p>
      </div>
    </div>
  );
}

export default App;
```

**Reflections (things to notice and think about):**

* Notice `className` instead of `class` (JavaScript reserved word)
* This JSX compiles to `React.createElement()` calls
* Compare this to vanilla JS: no `document.createElement()` or template strings needed
* The function returns what looks like HTML, but it's JSX

**Add some basic styling to `src/App.css`:**

```css
.App {
  max-width: 600px;
  margin: 50px auto;
  padding: 20px;
  font-family: Arial, sans-serif;
}

h1 {
  color: #333;
  border-bottom: 2px solid #4CAF50;
  padding-bottom: 10px;
}
```

---------

## Step 2: Extract a Reusable Component with Props

Now that we can examine our page in detail, we can isolate things that could be turned into components (legos/building-blocks). Things that are similar in nature or behavior make good candidates. In our case, the paragraph elements that are the list of things to do will be our starting point. We'll need to create a reusable structure (the component), then pass individual information/data (the props/properties) to customize the displayed information for each item on the list.

We'll create a collection of tasks, then supply that collection one at a time as props to the component definition. That way, we'll display the list as components, without having to define each one ourselves!

**Learning Points:**

* Props pass data from parent to child components
* Props are read-only (immutable)
* Use `.map()` to render lists from arrays
* Each list item needs a unique `key` prop

**Update `src/App.js`:**

```jsx
function TaskItem(props) {
  return (
    <div style={{ 
      padding: '10px', 
      margin: '10px 0', 
      border: '1px solid #ddd',
      borderRadius: '4px'
    }}>
      <p>{props.text}</p>
    </div>
  );
}

function App() {
  const tasks = [
    { id: 1, text: 'Learn React' },
    { id: 2, text: 'Build a project' },
    { id: 3, text: 'Deploy to production' }
  ];

  return (
    <div className="App">
      <h1>My Task List</h1>
      <div>
        {tasks.map(task => (
          <TaskItem key={task.id} text={task.text} />
        ))}
      </div>
    </div>
  );
}

export default App;
```

**Reflections:**

* `TaskItem` receives data through props
* Props are accessed via `props.text`, `props.id`, etc.
* The `key` prop helps React identify which items changed (important for performance)
* We're using JavaScript expressions inside JSX with curly braces `{}`
* Compare to vanilla JS: no loops with `innerHTML` or `appendChild`

-------

## Step 3: Add State and Interactivity

Now that we're leveraging components to display the list, what if the list changes? A to do list isn't much good if we can't add or remove things from it, so let's add some functionality. This will give us the ability to modify the collection of to-do items on the fly!

**Learning Points:**

* State is data that can change over time
* When state changes, React automatically re-renders the component
* `useState` returns current state and a function to update it
* React uses "controlled components" for form inputs

**Update `src/App.js`:**

```jsx
import { useState } from 'react';
import './App.css';

function TaskItem(props) {
  return (
    <div style={{ 
      padding: '10px', 
      margin: '10px 0', 
      border: '1px solid #ddd',
      borderRadius: '4px'
    }}>
      <p>{props.text}</p>
    </div>
  );
}

function App() {
  // State: current list of tasks
  const [tasks, setTasks] = useState([
    { id: 1, text: 'Learn React' },
    { id: 2, text: 'Build a project' },
    { id: 3, text: 'Deploy to production' }
  ]);

  // State: current value of input field
  const [newTaskText, setNewTaskText] = useState('');

  // Handler to add a new task
  const addTask = () => {
    if (newTaskText.trim() === '') return; // Don't add empty tasks

    const newTask = {
      id: Date.now(), // Simple way to generate unique IDs
      text: newTaskText
    };

    setTasks([...tasks, newTask]); // Add new task to array
    setNewTaskText(''); // Clear the input
  };

  return (
    <div className="App">
      <h1>My Task List</h1>
      
      {/* Input section */}
      <div style={{ marginBottom: '20px' }}>
        <input
          type="text"
          value={newTaskText}
          onChange={(e) => setNewTaskText(e.target.value)}
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

      {/* Task list */}
      <div>
        {tasks.map(task => (
          <TaskItem key={task.id} text={task.text} />
        ))}
      </div>
    </div>
  );
}

export default App;
```

**Reflections:**

1. **useState Hook:**
   * `const [tasks, setTasks] = useState(initialValue)`
   * `tasks` is the current state value
   * `setTasks` is the function to update state
   * Array destructuring syntax

2. **Controlled Component:**
   * Input's `value` is controlled by state (`newTaskText`)
   * `onChange` updates state on every keystroke
   * Compare to vanilla JS: no need to query the DOM with `document.querySelector`

3. **Event Handlers:**
   * `onClick={addTask}` - function reference, no parentheses
   * `onChange={(e) => setNewTaskText(e.target.value)}` - inline arrow function
   * `e.target.value` gets the input's current value

4. **Immutable State Updates:**
   * `setTasks([...tasks, newTask])` creates a NEW array
   * Never modify state directly (no `tasks.push()`)
   * React compares the old and new state to determine what changed

5. **Automatic Re-rendering:**
   * When `setTasks` is called, React re-renders the component
   * The new task appears automatically - no manual DOM manipulation!

---------

## Step 4: Add More Interactivity (Complete & Delete)

Last bit for now, let's be able to complete or just delete a task. The ideas are the same, but now when we modify the collection of items we'll be removing components that don't need to display any more, or that need to be modified based on a property (if it's complete).

**Learning Points:**

* Pass functions down to child components via props
* Update state immutably using array methods like `filter()` and `map()`
* Use conditional styling to show visual feedback

**Final `src/App.js`:**

```jsx
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
```

**Reflections:**

1. **Passing Functions as Props:**
   * `onToggle={toggleTask}` and `onDelete={deleteTask}`
   * Child components can trigger parent state changes
   * This is "lifting state up" - state lives in the parent, children notify parent of changes

2. **Immutable Updates with map():**

   ```jsx
   tasks.map(task =>
     task.id === id 
       ? { ...task, completed: !task.completed }  // Update this one
       : task                                      // Keep others unchanged
   )
   ```

   * Creates a new array with the updated task
   * Uses object spread to create a new task object

3. **Immutable Deletes with filter():**

   ```jsx
   tasks.filter(task => task.id !== id)
   ```

   * Returns a new array without the deleted task

4. **Conditional Rendering:**
   * Ternary operator: `{tasks.length === 0 ? <EmptyState /> : <TaskList />}`
   * Inline styles with conditional values: `textDecoration: task.completed ? 'line-through' : 'none'`

5. **Added keyboard support:**
   * `onKeyPress={(e) => e.key === 'Enter' && addTask()}`
   * Better UX - press Enter to add tasks

-------

### React vs Vanilla JavaScript

**Vanilla JS approach:**

```javascript
// Have to manually query DOM
const input = document.querySelector('#task-input');
const list = document.querySelector('#task-list');

// Manually create and append elements
const li = document.createElement('li');
li.textContent = input.value;
list.appendChild(li);

// Track state yourself
let tasks = [];
```

**React approach:**

```javascript
// State is declarative
const [tasks, setTasks] = useState([]);

// UI automatically updates when state changes
setTasks([...tasks, newTask]);

// No DOM manipulation needed!
```

### The React Mental Model

1. **Define your UI as a function of state**
   * Given this state, what should the UI look like?

2. **Update state, not the DOM**
   * Never use `document.querySelector` or manipulate DOM directly
   * Change state, React handles the rest

3. **Data flows down, events flow up**
   * Props pass data from parent to child
   * Functions passed as props let children communicate back

-------

## Next Steps: Connecting to ASP.NET

Remember that when we're using React, it's all based on JavaScript. Even if we opt to use Typescript for our React app, it will all transpile down to JS. So anything that we could have done in plain old JS, we can still do here! Including making a fetch request to an api! Say... to YOUR API! It's just a matter of pointing your request to the right place!

Preview what's coming next:

```jsx
// You'll fetch from your API...
useEffect(() => {
  fetch('https://localhost:7000/api/tasks')
    .then(res => res.json())
    .then(data => setTasks(data));
}, []);

// And send updates back...
const addTask = async (text) => {
  const response = await fetch('https://localhost:7000/api/tasks', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ text })
  });
  const newTask = await response.json();
  setTasks([...tasks, newTask]);
};
```
