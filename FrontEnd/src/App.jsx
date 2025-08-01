import './App.css'
import { Provider } from './components/ui/provider.jsx'
import { router } from './routes.jsx'
import { RouterProvider } from 'react-router'

function App() {
  return (
    <Provider>
      <RouterProvider router={router} />
    </Provider >
  )
}

export default App
