﻿using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Views;

namespace SampleApp.Droid
{
    class RepoAdapter : BaseAdapter<Repo>
    {

        Activity _context;
        public List<Repo> Repos { get; set; }

        public RepoAdapter(Activity context, List<Repo> repos)
        {
            this._context = context;
            this.Repos = repos;
        }

        public override int Count => Repos.Count;

        public override Repo this[int position] => Repos[position];

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            RepoAdapterViewHolder holder = null;
            var repo = this[position];

            if (view != null)
                holder = view.Tag as RepoAdapterViewHolder;

            if (holder == null)
            {
                holder = new RepoAdapterViewHolder();
                //var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //view = inflater.Inflate(Resource.Layout.RepoItem, parent, false);

                view = _context.LayoutInflater.Inflate(Resource.Layout.RepoItem, null);
                
                holder.NameTextView = view.FindViewById<TextView>(Resource.Id.nameTextView);
                holder.DescrTextView = view.FindViewById<TextView>(Resource.Id.descrTextView);
                holder.OwnerTextView = view.FindViewById<TextView>(Resource.Id.ownerTextView);
                holder.AvatarImageView = view.FindViewById<ImageViewAsync>(Resource.Id.avatarImageView);
                view.Tag = holder;
            }


            //fill in your items
            holder.NameTextView.Text = repo.Name;
            holder.DescrTextView.Text = repo.Description;
            holder.OwnerTextView.Text = $"Language: {repo.Language} Built By: {repo.Owner} - Stars: {repo.Stars}";
            ImageService.Instance.LoadUrl(repo.AvatarUrl)
                .LoadingPlaceholder("Icon.png")
                .Retry(3, 200)
                .DownSample(100, 100)
                .Into(holder.AvatarImageView);

            return view;
        }
    }

    class RepoAdapterViewHolder : Java.Lang.Object
    {
        public TextView NameTextView { get; set; }
        public TextView DescrTextView { get; set; }
        public TextView OwnerTextView { get; set; }
        public ImageViewAsync AvatarImageView { get; set; }
    }
}