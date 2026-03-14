import { createContext, useContext, useEffect, useMemo, useState } from 'react'
import { loginRequest } from '../services/authService'

const AuthContext = createContext(null)

export const AuthProvider = ({ children }) => {
  const [auth, setAuth] = useState(() => {
    const saved = localStorage.getItem('auth')
    return saved ? JSON.parse(saved) : null
  })

  useEffect(() => {
    if (auth) {
      localStorage.setItem('auth', JSON.stringify(auth))
      localStorage.setItem('token', auth.token)
    } else {
      localStorage.removeItem('auth')
      localStorage.removeItem('token')
    }
  }, [auth])

  const login = async (email, password) => {
    const response = await loginRequest(email, password)
    setAuth(response)
    return response
  }

  const logout = () => setAuth(null)

  const value = useMemo(
    () => ({
      auth,
      isAuthenticated: Boolean(auth?.token),
      roles: auth?.roles ?? [],
      login,
      logout
    }),
    [auth]
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export const useAuth = () => {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth must be used within AuthProvider')
  }
  return context
}
