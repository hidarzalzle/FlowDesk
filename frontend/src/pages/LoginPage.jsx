import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

export const LoginPage = () => {
  const navigate = useNavigate()
  const { login } = useAuth()

  const [form, setForm] = useState({ email: '', password: '' })
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  const onSubmit = async (event) => {
    event.preventDefault()
    setError('')
    setLoading(true)

    try {
      const authResponse = await login(form.email, form.password)
      if (authResponse.roles.includes('Admin')) {
        navigate('/admin')
      } else {
        navigate('/dashboard')
      }
    } catch {
      setError('Invalid login. Please use the seeded credentials below.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="auth-page">
      <form className="card" onSubmit={onSubmit}>
        <h1>Welcome back</h1>
        <p>Sign in to access your FlowDesk workspace.</p>

        <label htmlFor="email">Email</label>
        <input id="email" type="email" value={form.email} onChange={(e) => setForm({ ...form, email: e.target.value })} required />

        <label htmlFor="password">Password</label>
        <input id="password" type="password" value={form.password} onChange={(e) => setForm({ ...form, password: e.target.value })} required />

        {error && <p className="error">{error}</p>}

        <button type="submit" disabled={loading}>{loading ? 'Signing in...' : 'Sign in'}</button>

        <div className="hint">
          <p><strong>Demo credentials for testing:</strong></p>
          <p>Admin → admin@test.com / Admin123!</p>
          <p>User → user@test.com / User123!</p>
        </div>
      </form>
    </div>
  )
}
