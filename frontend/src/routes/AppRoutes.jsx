import { Navigate, Route, Routes } from 'react-router-dom'
import { LoginPage } from '../pages/LoginPage'
import { DashboardPage } from '../pages/DashboardPage'
import { ProfilePage } from '../pages/ProfilePage'
import { AdminPage } from '../pages/AdminPage'
import { NotAuthorizedPage } from '../pages/NotAuthorizedPage'
import { ProtectedRoute } from './ProtectedRoute'
import { AppLayout } from '../layouts/AppLayout'

export const AppRoutes = () => (
  <Routes>
    <Route path="/login" element={<LoginPage />} />
    <Route element={<ProtectedRoute />}>
      <Route element={<AppLayout />}>
        <Route path="/dashboard" element={<DashboardPage />} />
        <Route path="/profile" element={<ProfilePage />} />
      </Route>
    </Route>

    <Route element={<ProtectedRoute requiredRole="Admin" />}>
      <Route element={<AppLayout />}>
        <Route path="/admin" element={<AdminPage />} />
      </Route>
    </Route>

    <Route path="/not-authorized" element={<NotAuthorizedPage />} />
    <Route path="*" element={<Navigate to="/dashboard" replace />} />
  </Routes>
)
