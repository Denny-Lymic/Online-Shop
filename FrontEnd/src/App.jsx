import './App.css'
import { createBrowserRouter, RouterProvider } from 'react-router';
import { Provider } from './components/ui/provider.jsx'
import Login from './components/Pages/Login.jsx'
import { action as loginAction } from './requests/login.js';
import { loader as loadMainPage } from './requests/mainLoader.js';
import Registration from './components/Pages/Registration.jsx';
import { action as regAction } from './requests/registration.js';
import Main from './components/Pages/Main.jsx';
import { getProducts } from './requests/getProducts.js';

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
    loader: loadMainPage,
    action: getProducts
  }])

function App() {
  return (
    <Provider>
      <RouterProvider router={router} />
    </Provider >
  )
}

export default App
