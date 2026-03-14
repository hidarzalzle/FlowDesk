import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

export const NavBar = () => {
  const navigate = useNavigate()
  const { isAuthenticated, roles, logout } = useAuth()

  const onLogout = () => {
    logout()
    navigate('/login')
  }

  return (
    <header className="nav">
      <div className="brand">FlowDesk</div>
      <nav>
        {isAuthenticated && (
          <>
            <Link to="/dashboard">Dashboard</Link>
            <Link to="/profile">Profile</Link>
            {roles.includes('Admin') && <Link to="/admin">Admin</Link>}
            <button type="button" onClick={onLogout}>Logout</button>
          </>
        )}
      </nav>
    </header>
  )
}
