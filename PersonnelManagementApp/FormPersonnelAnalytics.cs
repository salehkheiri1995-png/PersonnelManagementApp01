                            if (e.ColumnIndex == dgv.Columns["Edit"].Index && e.RowIndex >= 0)
                            {
                                int personnelID = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["PersonnelID"].Value);
                                
                                // ✅ باز کردن FormPersonnelEdit
                                try
                                {
                                    FormPersonnelEdit editForm = new FormPersonnelEdit();
                                    editForm.txtPersonnelID.Text = personnelID.ToString();
                                    editForm.BtnLoad_Click(null, EventArgs.Empty);
                                    editForm.ShowDialog();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"❌ خطا در باز کردن فرم ویرایش: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }