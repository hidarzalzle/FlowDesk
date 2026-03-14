import api from './api'

export const fetchProfile = async () => {
  const { data } = await api.get('/users/me')
  return data
}

export const fetchUsers = async () => {
  const { data } = await api.get('/users')
  return data
}
