import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavigationBar from './navigation';
import Calculator from './calculator';
import ViewAll from './ViewAll';
import Search from './search';

function App() {
  return (
    <Router>
      <div>
        <NavigationBar/>
        <Routes>
          <Route path="/view-all" Component={ViewAll}/>
          <Route path="/" Component={Calculator}/>
          <Route path="/search" Component={Search}/>
        </Routes>
      </div>
    </Router>
  );
}

export default App;
