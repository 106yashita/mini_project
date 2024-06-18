
document.addEventListener("DOMContentLoaded", function() {
  if (window.location.pathname.endsWith('admin.html')) {
      fetchEvents();
  }
});

let events=[];
let menu = document.querySelector("#menu-bars");
let navbar = document.querySelector(".navbar");

menu.onclick = () => {
  menu.classList.toggle("fa-times");
  navbar.classList.toggle("active");
};

window.onscroll = () => {
  menu.classList.remove("fa-times");
  navbar.classList.remove("active");
};

var swiper = new Swiper(".home-slider", {
  effect: "coverflow",
  grabCursor: true,
  centeredSlides: true,
  slidesPerView: "auto",
  coverflowEffect: {
    rotate: 0,
    stretch: 0,
    depth: 100,
    modifier: 3,
    slideShadows: true,
  },
  loop: true,
  autoplay: {
    delay: 3000,
    disableOnInteraction: false,
  },
});

var swiper = new Swiper(".review-slider", {
  slidesPerView: 1,
  grabCursor: true,
  loop: true,
  spaceBetween: 10,
  breakpoints: {
    0: {
      slidesPerView: 1,
    },
    700: {
      slidesPerView: 2,
    },
    1050: {
      slidesPerView: 3,
    },
  },
  autoplay: {
    delay: 5000,
    disableOnInteraction: false,
  },
});

function SignIn(){
  const user = document.getElementById('signin-user').value;
  const pass = document.getElementById('signin-pass').value;
  fetch('http://localhost:5168/api/Auth/Login', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
        },
    body: JSON.stringify({
        "userName": user,
        "password": pass
    })
  })
  .then(res => res.json())
  .then(data=>{
    console.log(data);
    localStorage.setItem('token',data.token);
  });
}

function SignUp(){
  const user = document.getElementById('signup-user').value;
  const pass = document.getElementById('signup-pass').value;
  const email = document.getElementById('signup-email').value;
  const userType = document.getElementById('signup-usertype').value;
  fetch('http://localhost:5168/api/Auth/Register', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
          },
      body: JSON.stringify({
        "userName": user,
        "email": email,
        "userType": userType,
        "password": pass
      })
    })
    .then(res => res.json())
    .then(data=>{
      console.log(data);
    });
}

function CreateEvent(){
  const eventName = document.getElementById('eventName').value;
  const eventDescription = document.getElementById('eventDescription').value;
  const eventType = document.getElementById('eventType').value;
  const eventLocation = document.getElementById('eventLocation').value;
  fetch('http://localhost:5168/api/Admin/events', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwiZXhwIjoxNzE4ODc2ODcwfQ.uDTALnJQGmT5NKd-QNwghwQx--knkIgHwDXin33b9jM', 
     }, 
      body: JSON.stringify({
        "eventName": eventName,
        "description": eventDescription,
        "eventType": eventType,
        "location": eventLocation
      })
    })
    .then(res => res.json())
    .then(data=>{
      console.log(data);
      fetchEvents();
    });
}



function fetchEvents() {
  fetch('http://localhost:5168/api/Admin/events', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwiZXhwIjoxNzE4ODc2ODcwfQ.uDTALnJQGmT5NKd-QNwghwQx--knkIgHwDXin33b9jM', 
       }, 
  })
  .then(response => response.json())
  .then(data => {
    events=data;
      displayEvents(data);
  })
  .catch(error => {
      console.error('Error fetching events:', error);
  });
}

function displayEvents(events) {
  const eventCardsContainer = document.getElementById('eventCardsContainer');
  eventCardsContainer.innerHTML = '';

  events.forEach(event => {
      const card = document.createElement('div');
      card.className = 'card mb-3';
      card.style = 'max-width: 350px;';

      card.innerHTML = `
          <div class="row g-0">
                  <div class="card-body">
                      <h5 class="card-title">${event.eventName}</h5>
                      <p class="card-text">${event.eventDescription}</p>
                      <p class="card-text">Type: ${event.eventType}</p>
                      <p class="card-text">Location: ${event.location}</p>
                      <p class="card-text">CreatedAt: ${event.date}</p>
                      <button type="button" class="btn" onclick="openUpdateModal(${event.eventId})">Update</button>
                  </div>                
          </div>
      `;

      eventCardsContainer.appendChild(card);
  });
}

function openUpdateModal(eventId) {
  // Find the event details
  const event = events.find(e => e.eventId === eventId);

  // Pre-fill the form with event details
  document.getElementById('updateEventId').value = event.eventId;
  document.getElementById('updateEventName').value = event.eventName;
  document.getElementById('updateEventDescription').value = event.eventDescription;

  // Show the modal
  const updateEventModal = new bootstrap.Modal(document.getElementById('updateEventModal'));
  updateEventModal.show();
}

function updateEvent() {
  const eventId = document.getElementById('updateEventId').value;
  const eventName = document.getElementById('updateEventName').value;
  const eventDescription = document.getElementById('updateEventDescription').value;

  fetch(`http://localhost:5168/api/Admin/events`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwiZXhwIjoxNzE4ODc2ODcwfQ.uDTALnJQGmT5NKd-QNwghwQx--knkIgHwDXin33b9jM', 
       }, 
      body: JSON.stringify({
          eventId:eventId,
          eventName: eventName,
          description: eventDescription
      })
  })
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.text(); // Get the response body as text
  })
  .then(data => {
    console.log('Event updated:', data); // Log the text response
    const updateEventModal = bootstrap.Modal.getInstance(document.getElementById('updateEventModal'));
    updateEventModal.hide();
    fetchEvents();
  })
  .catch(error => console.error('Error updating event:', error));
}

