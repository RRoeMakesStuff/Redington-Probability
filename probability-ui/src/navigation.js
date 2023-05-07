import React from 'react';

function NavigationBar(props) {
  return (
    <nav>
      <ul>
        <li><a href="/">Calculate</a></li>
        <li><a href="view-all">View All</a></li>
        <li><a href="search">Search</a></li>
      </ul>
    </nav>
  );
}

export default NavigationBar;