import { Navigate, Outlet } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

export const ProtectedRoute = ({ requiredRole }) => {
  const { isAuthenticated, roles } = useAuth()

  if (!isAuthenticated) return <Navigate to="/login" replace />
  if (requiredRole && !roles.includes(requiredRole)) return <Navigate to="/not-authorized" replace />

  return <Outlet />
}
