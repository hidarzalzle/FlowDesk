import { useEffect, useState } from 'react'
import { createContent, deleteContent, fetchContent, updateContent } from '../services/contentService'
import { fetchUsers } from '../services/userService'

const initialForm = { title: '', description: '', isPublished: true }

export const AdminPage = () => {
  const [items, setItems] = useState([])
  const [users, setUsers] = useState([])
  const [form, setForm] = useState(initialForm)
  const [editingId, setEditingId] = useState(null)

  const loadData = async () => {
    const [contentData, usersData] = await Promise.all([fetchContent(), fetchUsers()])
    setItems(contentData)
    setUsers(usersData)
  }

  useEffect(() => {
    loadData().catch(() => undefined)
  }, [])

  const handleSubmit = async (event) => {
    event.preventDefault()
    if (editingId) {
      await updateContent(editingId, form)
    } else {
      await createContent(form)
    }

    setForm(initialForm)
    setEditingId(null)
    await loadData()
  }

  return (
    <section>
      <h2>Admin Management</h2>
      <p>Manage content and inspect all registered users.</p>

      <form className="card" onSubmit={handleSubmit}>
        <h3>{editingId ? 'Edit item' : 'Create new item'}</h3>
        <input placeholder="Title" value={form.title} onChange={(e) => setForm({ ...form, title: e.target.value })} required />
        <textarea placeholder="Description" value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} required />
        <label>
          <input type="checkbox" checked={form.isPublished} onChange={(e) => setForm({ ...form, isPublished: e.target.checked })} />
          Published
        </label>
        <button type="submit">{editingId ? 'Update' : 'Create'}</button>
      </form>

      <div className="grid">
        {items.map((item) => (
          <article key={item.id} className="card item-card">
            <h4>{item.title}</h4>
            <p>{item.description}</p>
            <div className="row-actions">
              <button onClick={() => { setEditingId(item.id); setForm(item) }}>Edit</button>
              <button onClick={async () => { await deleteContent(item.id); await loadData() }}>Delete</button>
            </div>
          </article>
        ))}
      </div>

      <section className="card">
        <h3>All users</h3>
        {users.map((user) => (
          <p key={user.id}>{user.fullName} — {user.email} ({user.roles.join(', ')})</p>
        ))}
      </section>
    </section>
  )
}
