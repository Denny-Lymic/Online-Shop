import './App.css'
import { createBrowserRouter, RouterProvider } from 'react-router';
import { Provider } from './components/ui/provider.jsx'
import Login, { action as loginAction } from './components/Pages/Login.jsx'
import getProfile from './assets/requests/profile.js';
import Registration, { action as regAction } from './components/Pages/Registration.jsx';
import Main from './components/Pages/Main.jsx';

const router = createBrowserRouter([
  {
    path: "/login",
    element: <Login />,
    action: loginAction
  },
  {
    path: "/registration",
    element: <Registration />,
    action: regAction
  },
  {
    path: "/",
    element: <Main />,
    loader: getProfile
  }])

function App() {
  return (
    <Provider>
      <RouterProvider router={router} />
    </Provider >
  )
}

export default App
