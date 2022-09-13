from flask import Blueprint, render_template, request
from flask_login import login_required, current_user
from .export import generate_file
from .models import db

views = Blueprint("views", __name__)

@views.route("/", methods=["GET", "POST"])
def home():
    if request.method == "POST":
        db.session.remove()


    return render_template("home.html", user=current_user)