import { Link } from 'react-router-dom'

export const NotAuthorizedPage = () => (
  <section className="card">
    <h2>Not authorized</h2>
    <p>You do not have permission to view this page.</p>
    <Link to="/dashboard">Return to dashboard</Link>
  </section>
)
