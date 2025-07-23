'use client';

import { useState, useEffect } from 'react';
import { fetchConfigurations, updateItem, createConfiguration } from '@/lib/api';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  IconButton,
  Typography,
  Chip
} from '@mui/material';
import { Add, Edit } from '@mui/icons-material';
import DataModal from './dataModal';

export default function DataTable() {
  const [items, setItems] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentItem, setCurrentItem] = useState(null);
  const [isEdit, setIsEdit] = useState(false);

  useEffect(() => {
    const loadItems = async () => {
      const data = await fetchConfigurations();
      setItems(data);
    };
    loadItems();
  }, []);

  const handleEdit = (item) => {
    setCurrentItem(item);
    setIsEdit(true);
    setIsModalOpen(true);
  };

  const handleAdd = () => {
    setCurrentItem({
      name: '',
      type: '',
      value: '',
      isActive: true,
      applicationName: ''
    });
    setIsEdit(false);
    setIsModalOpen(true);
  };

  const handleUpdate = async (updatedData) => {
    try {
      // ID'yi kontrol et
      if (!
        updatedData.id
      ) {
        console.error('Update error: ID is missing');
        return;
      }

      const updatedItem = await updateItem(
        updatedData.id
        , updatedData);
      setItems(
        items.map
          (item =>
            item.id
              ===
              updatedData.id
              ? updatedItem : item));
    } catch (error) {
      console.error('Update failed:', error);
    }
  };


  const handleCreate = async (newData) => {
    try {
      const createdItem = await createConfiguration(newData);
      setItems([createdItem, ...items]);
    } catch (error) {
      console.error('Create failed:', error);
    }
  };

  const handleSubmit = isEdit ? handleUpdate : handleCreate;

  return (
    <div className="container mx-auto p-4">
      <div className="flex justify-between items-center mb-6">
        <Typography variant="h4" className='text-black' component="h1">
          Configurations
        </Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={handleAdd}
          className="bg-blue-600 hover:bg-blue-700"
        >
          Add Configuration
        </Button>
      </div>

      <TableContainer component={Paper}>
        <Table>
          <TableHead className="bg-gray-100">
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Type</TableCell>
              <TableCell>Value</TableCell>
              <TableCell>Is Active</TableCell>
              <TableCell>Application Name</TableCell>
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((item) => (
              <TableRow key={item.id} hover>
                <TableCell>{item.id}</TableCell>
                <TableCell>{item.name}</TableCell>
                <TableCell>{item.type}</TableCell>
                <TableCell>{item.value}</TableCell>
                <TableCell>
                  <Chip
                    label={item.isActive ? 'Active' : 'Inactive'}
                    color={item.isActive ? 'success' : 'error'}
                    size="small"
                  />
                </TableCell>
                <TableCell>{item.applicationName}</TableCell>
                <TableCell>
                  <IconButton
                    color="primary"
                    onClick={() => handleEdit(item)}
                    aria-label="edit"
                  >
                    <Edit />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      <DataModal
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSubmit={handleSubmit}
        initialData={currentItem}
        isEdit={isEdit}
      />
    </div>
  );
}