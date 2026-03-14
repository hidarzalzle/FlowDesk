import { useEffect, useState } from 'react'
import { fetchProfile } from '../services/userService'

export const ProfilePage = () => {
  const [profile, setProfile] = useState(null)

  useEffect(() => {
    fetchProfile().then(setProfile).catch(() => undefined)
  }, [])

  if (!profile) return <p>Loading profile...</p>

  return (
    <section className="card">
      <h2>Profile</h2>
      <p><strong>Name:</strong> {profile.fullName}</p>
      <p><strong>Email:</strong> {profile.email}</p>
      <p><strong>Roles:</strong> {profile.roles.join(', ')}</p>
    </section>
  )
}
