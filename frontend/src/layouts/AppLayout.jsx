import { Outlet } from 'react-router-dom'
import { NavBar } from '../components/NavBar'

export const AppLayout = () => (
  <div className="app-shell">
    <NavBar />
    <main className="page-container">
      <Outlet />
    </main>
  </div>
)
