// Стили
import './App.css';

// Библиотеки
import { createBrowserRouter } from 'react-router';

// Страницы
import Login from './components/Pages/Login.jsx';
import Registration from './components/Pages/Registration.jsx';
import Main from './components/Pages/Main.jsx';
import CreateProduct from './components/Pages/CreateProduct.jsx';
import ProductLayout from './components/Pages/ProductLayout.jsx';
import ProductDetails from './components/Pages/ProductDetails.jsx';
import UpdateProduct from './components/Pages/UpdateProduct.jsx';

// Actions и Loaders
import { action as loginAction } from './requests/login.js';
import { action as regAction } from './requests/registration.js';
import { loader as loadMainPage } from './requests/mainLoader.js';
import { getProducts } from './requests/getProducts.js';
import { loader as loadUser } from './requests/createLoader.js';
import { createProduct } from './requests/createProduct.js';
import { loader as loadProduct } from './requests/loadProduct.js';
import { updateProduct } from './requests/updateProduct.js';

export const router = createBrowserRouter([
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
    },
    {
        path: "/create",
        element: <CreateProduct />,
        loader: loadUser,
        action: createProduct
    },
    {
        path: "/product/:id",
        element: <ProductLayout />,
        loader: loadProduct,
        children: [
            {
                index: true,
                element: <ProductDetails />
            },
            {
                path: "update",
                element: <UpdateProduct />,
                action: updateProduct,
            }
        ]
    },
])