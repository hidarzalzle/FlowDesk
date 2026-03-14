import api from './api'

export const fetchContent = async () => {
  const { data } = await api.get('/content')
  return data
}

export const createContent = async (payload) => {
  const { data } = await api.post('/content', payload)
  return data
}

export const updateContent = async (id, payload) => {
  const { data } = await api.put(`/content/${id}`, payload)
  return data
}

export const deleteContent = async (id) => {
  await api.delete(`/content/${id}`)
}
