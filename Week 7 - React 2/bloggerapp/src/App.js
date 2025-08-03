import React, { useState } from 'react';
import './App.css';

// Data
const books = [
  { id: 101, bname: 'Master React', price: 670 },
  { id: 102, bname: 'Deep Dive into Angular 11', price: 800 },
  { id: 103, bname: 'Mongo Essentials', price: 450 }
];

const courses = [
  { name: 'Angular', date: '4/5/2021' },
  { name: 'React', date: '6/3/2021' }
];

const blogs = [
  { 
    title: 'React Learning', 
    author: 'Stephen Biz',
    content: 'Welcome to learning React!'
  },
  {
    title: 'Installation',
    author: 'Schewzdenier', 
    content: 'You can install React from npm.'
  }
];

// Components
const BookDetails = ({ books }) => (
  <div className="column">
    <h2>Book Details</h2>
    {books.map(book => (
      <div key={book.id} className="item">
        <h3>{book.bname}</h3>
        <p>{book.price}</p>
      </div>
    ))}
  </div>
);

const CourseDetails = ({ courses }) => (
  <div className="column">
    <h2>Course Details</h2>
    {courses.map((course, index) => (
      <div key={index} className="item">
        <h3>{course.name}</h3>
        <p>{course.date}</p>
      </div>
    ))}
  </div>
);

const BlogDetails = ({ blogs }) => (
  <div className="column">
    <h2>Blog Details</h2>
    {blogs.map((blog, index) => (
      <div key={index} className="item">
        <h3>{blog.title}</h3>
        <p className="author">{blog.author}</p>
        <p className="content">{blog.content}</p>
      </div>
    ))}
  </div>
);

// Main App
function App() {
  const [showContent, setShowContent] = useState(true);

  return (
    <div className="App">
      {showContent && (
        <div className="container">
          <CourseDetails courses={courses} />
          <BookDetails books={books} />
          <BlogDetails blogs={blogs} />
        </div>
      )}
    </div>
  );
}

export default App;