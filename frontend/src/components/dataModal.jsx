'use client';

import { useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  Box,
  FormControlLabel,
  Checkbox,
  MenuItem,
  Select,
  InputLabel,
  FormControl
} from '@mui/material';

const itemTypes = ['Module', 'Feature', 'Service', 'Component'];
const applications = ['Admin Panel', 'Analytics', 'CRM', 'E-commerce'];

export default function DataModal({ open, onClose, onSubmit, initialData, isEdit }) {
  const [formData, setFormData] = useState(initialData || {
    name: '',
    type: '',
    value: '',
    isActive: true,
    applicationName: ''
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleCheckboxChange = (e) => {
    const { name, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: checked
    }));
  };

  const handleSubmit = () => {
    // ID'yi koruyarak form verilerini gönder
    const submitData = isEdit ? {
      ...formData, id:
        initialData.id
    } : formData;
    onSubmit(submitData);
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>{isEdit ? 'Update Configuration' : 'New Configuration'}</DialogTitle>
      <DialogContent>
        <Box component="form" sx={{ mt: 2 }}>
          <TextField
            fullWidth
            margin="normal"
            label="Name"
            name="name"
            value={formData.name}
            onChange={handleChange}
            required
          />
          <TextField
            fullWidth
            margin="normal"
            label="Type"
            name="type"
            value={formData.type}
            onChange={handleChange}
            required
          />

          <TextField
            fullWidth
            margin="normal"
            label="Value"
            name="value"
            value={formData.value}
            onChange={handleChange}
            required
          />

          <TextField
            fullWidth
            margin="normal"
            label="Application Name"
            name="applicationName"
            value={formData.applicationName}
            onChange={handleChange}
            required
          />

          <FormControlLabel
            control={
              <Checkbox
                name="isActive"
                checked={formData.isActive}
                onChange={handleCheckboxChange}
                color="primary"
              />
            }
            label="Is Active"
            sx={{ mt: 2 }}
          />
        </Box>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button onClick={handleSubmit} variant="contained" color="primary">
          {isEdit ? 'Update' : 'Create'}
        </Button>
      </DialogActions>
    </Dialog>
  );
}