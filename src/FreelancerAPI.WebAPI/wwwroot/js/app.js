const API_BASE_URL = '/api/freelancers';
let currentFreelancers = [];

// Load freelancers when page loads
document.addEventListener('DOMContentLoaded', function() {
    loadFreelancers();
});

// Load all freelancers
async function loadFreelancers() {
    try {
        const response = await fetch(API_BASE_URL);
        currentFreelancers = await response.json();
        displayFreelancers(currentFreelancers);
    } catch (error) {
        console.error('Error loading freelancers:', error);
        showAlert('Error loading freelancers', 'danger');
    }
}

// Display freelancers
function displayFreelancers(freelancers) {
    const container = document.getElementById('freelancersList');
    
    if (freelancers.length === 0) {
        container.innerHTML = '<div class="col-12"><div class="alert alert-info">No freelancers found.</div></div>';
        return;
    }

    container.innerHTML = freelancers.map(freelancer => `
        <div class="col-md-6 col-lg-4">
            <div class="card freelancer-card ${freelancer.isArchived ? 'archived' : ''}">
                <div class="card-body">
                    <h5 class="card-title">
                        ${freelancer.username}
                        ${freelancer.isArchived ? '<span class="badge bg-danger ms-2">Archived</span>' : ''}
                    </h5>
                    <p class="card-text">
                        <strong>Email:</strong> ${freelancer.email}<br>
                        <strong>Phone:</strong> ${freelancer.phoneNumber}
                    </p>
                    
                    ${freelancer.skillsets.length > 0 ? `
                        <p><strong>Skillsets:</strong><br>
                        ${freelancer.skillsets.map(skill => `<span class="skillset">${skill}</span>`).join('')}</p>
                    ` : ''}
                    
                    ${freelancer.hobbies.length > 0 ? `
                        <p><strong>Hobbies:</strong><br>
                        ${freelancer.hobbies.map(hobby => `<span class="hobby">${hobby}</span>`).join('')}</p>
                    ` : ''}
                    
                    <div class="btn-group-vertical d-grid gap-2">
                        <button class="btn btn-outline-primary btn-sm" onclick="editFreelancer(${freelancer.id})">
                            Edit
                        </button>
                        ${freelancer.isArchived ? 
                            `<button class="btn btn-outline-success btn-sm" onclick="unarchiveFreelancer(${freelancer.id})">
                                Unarchive
                            </button>` :
                            `<button class="btn btn-outline-warning btn-sm" onclick="archiveFreelancer(${freelancer.id})">
                                Archive
                            </button>`
                        }
                        <button class="btn btn-outline-danger btn-sm" onclick="deleteFreelancer(${freelancer.id})">
                            Delete
                        </button>
                    </div>
                </div>
                <div class="card-footer text-muted">
                    <small>Created: ${new Date(freelancer.createdAt).toLocaleDateString()}</small>
                </div>
            </div>
        </div>
    `).join('');
}

// Show add modal
function showAddModal() {
    document.getElementById('modalTitle').textContent = 'Add New Freelancer';
    document.getElementById('freelancerForm').reset();
    document.getElementById('freelancerId').value = '';
    new bootstrap.Modal(document.getElementById('freelancerModal')).show();
}

// Edit freelancer
async function editFreelancer(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/${id}`);
        const freelancer = await response.json();
        
        document.getElementById('modalTitle').textContent = 'Edit Freelancer';
        document.getElementById('freelancerId').value = freelancer.id;
        document.getElementById('username').value = freelancer.username;
        document.getElementById('email').value = freelancer.email;
        document.getElementById('phoneNumber').value = freelancer.phoneNumber;
        document.getElementById('skillsets').value = freelancer.skillsets.join(', ');
        document.getElementById('hobbies').value = freelancer.hobbies.join(', ');
        
        new bootstrap.Modal(document.getElementById('freelancerModal')).show();
    } catch (error) {
        console.error('Error loading freelancer:', error);
        showAlert('Error loading freelancer data', 'danger');
    }
}

// Save freelancer (add or update)
async function saveFreelancer() {
    const form = document.getElementById('freelancerForm');
    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    const id = document.getElementById('freelancerId').value;
    const data = {
        username: document.getElementById('username').value,
        email: document.getElementById('email').value,
        phoneNumber: document.getElementById('phoneNumber').value,
        skillsets: document.getElementById('skillsets').value.split(',').map(s => s.trim()).filter(s => s),
        hobbies: document.getElementById('hobbies').value.split(',').map(h => h.trim()).filter(h => h)
    };

    try {
        const url = id ? `${API_BASE_URL}/${id}` : API_BASE_URL;
        const method = id ? 'PUT' : 'POST';
        
        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            bootstrap.Modal.getInstance(document.getElementById('freelancerModal')).hide();
            showAlert(`Freelancer ${id ? 'updated' : 'created'} successfully!`, 'success');
            loadFreelancers();
        } else {
            throw new Error('Failed to save freelancer');
        }
    } catch (error) {
        console.error('Error saving freelancer:', error);
        showAlert('Error saving freelancer', 'danger');
    }
}

// Delete freelancer
async function deleteFreelancer(id) {
    if (!confirm('Are you sure you want to delete this freelancer?')) {
        return;
    }

    try {
        const response = await fetch(`${API_BASE_URL}/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            showAlert('Freelancer deleted successfully!', 'success');
            loadFreelancers();
        } else {
            throw new Error('Failed to delete freelancer');
        }
    } catch (error) {
        console.error('Error deleting freelancer:', error);
        showAlert('Error deleting freelancer', 'danger');
    }
}

// Archive freelancer
async function archiveFreelancer(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/${id}/archive`, {
            method: 'POST'
        });

        if (response.ok) {
            showAlert('Freelancer archived successfully!', 'success');
            loadFreelancers();
        } else {
            throw new Error('Failed to archive freelancer');
        }
    } catch (error) {
        console.error('Error archiving freelancer:', error);
        showAlert('Error archiving freelancer', 'danger');
    }
}

// Unarchive freelancer
async function unarchiveFreelancer(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/${id}/unarchive`, {
            method: 'POST'
        });

        if (response.ok) {
            showAlert('Freelancer unarchived successfully!', 'success');
            loadFreelancers();
        } else {
            throw new Error('Failed to unarchive freelancer');
        }
    } catch (error) {
        console.error('Error unarchiving freelancer:', error);
        showAlert('Error unarchiving freelancer', 'danger');
    }
}

// Search freelancers
async function searchFreelancers() {
    const searchTerm = document.getElementById('searchInput').value.trim();
    
    if (!searchTerm) {
        loadFreelancers();
        return;
    }

    try {
        const response = await fetch(`${API_BASE_URL}/search?term=${encodeURIComponent(searchTerm)}`);
        const results = await response.json();
        displayFreelancers(results);
    } catch (error) {
        console.error('Error searching freelancers:', error);
        showAlert('Error searching freelancers', 'danger');
    }
}

// Show alert
function showAlert(message, type) {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;
    
    document.querySelector('.container').insertBefore(alertDiv, document.querySelector('.container').firstChild);
    
    setTimeout(() => {
        alertDiv.remove();
    }, 5001);
}

// Search on Enter key
document.getElementById('searchInput').addEventListener('keypress', function(e) {
    if (e.key === 'Enter') {
        searchFreelancers();
    }
});