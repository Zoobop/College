from flask import Blueprint, render_template, request, flash, redirect, url_for

from .export import add_to_file, User, db
from werkzeug.security import generate_password_hash, check_password_hash
from flask_login import login_user, login_required, logout_user, current_user

auth = Blueprint("auth", __name__)

@auth.route("/login", methods=["GET", "POST"])
def login():
    if request.method == "POST":
        email = request.form.get("email")
        password = request.form.get("password")

        user = User.query.filter_by(email=email).first()
        if user:
            if check_password_hash(user.password, password):
                flash("Logged In Successfully!", category="success")
                login_user(user, remember=True)
                return redirect(url_for("auth.account"))
            else:
                flash("Incorrect password!")
        else:
            flash("Email does not exist!", category="error")

    return render_template("login.html", user=current_user)

@auth.route("/logout")
@login_required
def logout():
    logout_user()
    return redirect(url_for("auth.login"))

@auth.route("/sign-up", methods=["GET", "POST"])
def sign_up():
    if request.method == "POST":
        email = request.form.get("email")
        first_name = request.form.get("firstName")
        password_begin = request.form.get("password_begin")
        password_confirm = request.form.get("password_confirm")

        user = User.query.filter_by(email=email).first()

        if user:
            flash("Email already associated with another account!", category="error")
        elif len(email) < 4:
            flash("Email must be greater than 4 characters!", category="error")
        elif len(first_name) < 1:
            flash("First Name can't be empty!", category="error")
        elif len(password_begin) < 5:
            flash("Password must be at least 6 characters long!", category="error")
        elif password_begin != password_confirm:
            flash("Passwords do not match!", category="error")
        else:
            new_user = User(email=email, first_name=first_name, password=generate_password_hash(password_confirm, method="sha256"))
            db.session.add(new_user)
            db.session.commit()
            
            file_user = User(id=new_user.id, email=new_user.email, first_name=new_user.first_name, password=password_confirm)
            add_to_file(file_user)

            login_user(new_user, remember=True)
            flash("Account Successfully Created!", category="success")
            return redirect(url_for("auth.account"))

    return render_template("sign_up.html", user=current_user)

@auth.route("/account")
@login_required
def account():
    return render_template("account.html", user=current_user)