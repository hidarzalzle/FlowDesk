import { useEffect, useState } from 'react'
import { fetchContent } from '../services/contentService'

export const DashboardPage = () => {
  const [items, setItems] = useState([])
  const [status, setStatus] = useState('loading')

  useEffect(() => {
    fetchContent()
      .then((data) => {
        setItems(data)
        setStatus('ready')
      })
      .catch(() => setStatus('error'))
  }, [])

  return (
    <section>
      <h2>Dashboard</h2>
      <p>Recent content available to your role.</p>
      {status === 'loading' && <p>Loading content...</p>}
      {status === 'error' && <p className="error">Could not load content from the API.</p>}
      <div className="grid">
        {items.map((item) => (
          <article key={item.id} className="card item-card">
            <h3>{item.title}</h3>
            <p>{item.description}</p>
            <small>{item.isPublished ? 'Published' : 'Draft'}</small>
          </article>
        ))}
      </div>
    </section>
  )
}
