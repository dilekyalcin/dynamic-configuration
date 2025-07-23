import axios from 'axios';

const API_URL = process.env.NEXT_PUBLIC_API_URL;

export const fetchConfigurations = async () => {
  try {
    const response = await axios.get(API_URL + 'ConfigurationReaders/all-configs');
    return response.data;
  } catch (error) {
    console.error('Fetch error:', error);
    return [
      {
        id: 1,
        name: "Kullanıcı Yönetimi",
        type: "Module",
        value: "user-management",
        isActive: true,
        applicationName: "Admin Panel"
      },
      {
        id: 2,
        name: "Raporlar",
        type: "Feature",
        value: "reports",
        isActive: false,
        applicationName: "Analytics"
      }
    ];
  }
};

export const updateItem = async (id, data) => {
  try {
    const response = await axios.put(`${API_URL}/ConfigurationReaders/${id}`, data);
    return response.data;
  } catch (error) {
    console.error('Update error:', error);
    return { ...data, id };
  }
};

export const createConfiguration = async (data) => {
  try {
    const response = await axios.post(API_URL + 'ConfigurationReaders', data);
    return response.data;
  } catch (error) {
    console.error('Create error:', error);
    return { ...data, id: Math.floor(Math.random() * 1000) };
  }
};